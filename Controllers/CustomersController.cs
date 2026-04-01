using ManvarFitness.Database;
using ManvarFitness.Entity;
using ManvarFitness.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Rotativa.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Rotativa.AspNetCore.Options;
using System;

namespace ManvarFitness.Controllers
{
    public class CustomersController : BaseController
    {
        private List<string> SafeDeserialize(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return new List<string>();

            try
            {
                return JsonConvert.DeserializeObject<List<string>>(value) ?? new List<string>();
            }
            catch
            {
                return value.Split(',').Select(x => x.Trim()).ToList();
            }
        }

        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public CustomersController(ApplicationDbContext context, IWebHostEnvironment env) : base(context)
        {
            _context = context;
            _env = env;
        }

        // GET: UserController
        public IActionResult Index()
        {
            var data = (from u in _context.Users
                        join s in _context.UserSubscriptions
                            on u.UserId equals s.UserId into subs
                        from s in subs.OrderByDescending(x => x.EndDate).Take(1).DefaultIfEmpty()

                        join p in _context.SubscriptionPlans
                            on s.PlanId equals p.PlanId into subscriptionPlans
                        from p in subscriptionPlans.DefaultIfEmpty()

                        join pa in _context.Payments
                            on s.SubscriptionId equals pa.SubscriptionId into payments
                        from pa in payments.DefaultIfEmpty()

                        select new UserModel
                        {
                            UserId = u.UserId,
                            Name = u.Name,
                            Gender = u.Gender,
                            Mobile = u.Mobile,
                            IsActive = u.IsActive,

                            SubscriptionId = s != null ? s.SubscriptionId : null,
                            PlanName = p != null ? p.Name : "Not Updated",
                            Amount = pa != null ? pa.Amount : 0,
                            Currency = pa != null ? pa.Currency : "N/A",
                            PaymentGateway = pa != null ? pa.PaymentGateway : "N/A"
                        }).ToList();

            return View(data);
        }

        public IActionResult SubscriptionDetails(long id)
        {
            var subscriptions = (from us in _context.UserSubscriptions
                                 join u in _context.Users on us.UserId equals u.UserId
                                 join p in _context.SubscriptionPlans on us.PlanId equals p.PlanId
                                 join pa in _context.Payments
                                      on us.SubscriptionId equals pa.SubscriptionId into payments
                                 from pa in payments.DefaultIfEmpty()
                                 where us.SubscriptionId == id
                                 select new UserSubscriptionModel
                                 {
                                     SubscriptionId = us.SubscriptionId,
                                     UserName = u.Name,
                                     PlanName = p.Name,
                                     StartDate = us.StartDate,
                                     EndDate = us.EndDate,

                                     Amount = pa != null ? pa.Amount : 0,
                                     Currency = pa != null ? pa.Currency : "N/A",
                                     PaymentGateway = pa != null ? pa.PaymentGateway : "N/A"

                                 }).FirstOrDefault();
            if (subscriptions == null)
                return NotFound();

            subscriptions.Status = subscriptions.EndDate < DateTime.UtcNow ? "Expired" : "Active";
            return View(subscriptions);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ToggleActive(Guid id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
                return Json(new { success = false });

            user.IsActive = !user.IsActive;
            _context.SaveChanges();

            return Json(new { success = true, isActive = user.IsActive });
        }

        [HttpGet]
        public IActionResult AssignDietPlan(Guid id, int? concernId, int? subconcernId)
        {
            var user = _context.Users.Find(id);
            if (user == null)
                return NotFound();

            var concerns = _context.Concerns.ToList();

            var subconcerns = concernId.HasValue
                ? _context.SubConcerns.Where(x => x.ConcernId == concernId.Value).ToList()
                : new List<SubConcerns>();

            DietPlanModel model = null;


            var latestplan = _context.UserDietPlans
                .Where(x => x.UserId == id && x.UserConcernId == concernId && x.IsLatest )
                .OrderByDescending(x => x.Version)
                .FirstOrDefault();

            if (latestplan != null && !string.IsNullOrEmpty(latestplan.DietPlanData) && latestplan.UserConcernId == concernId)
            {
                model = JsonConvert.DeserializeObject<DietPlanModel>(latestplan.DietPlanData);
                model.PdfUrl = latestplan.PdfUrl;
            }
            else
            {
                // 2. Load MASTER plan if no user plan
                var masterPlans = _context.DietPlans
                    .Where(x => x.ConcernId == concernId && !x.IsDeleted && x.IsActive)
                    .ToList();

                if (masterPlans.Any())
                {
                    model = new DietPlanModel
                    {
                        UserId = id,
                        ConcernId = concernId ?? 0,
                        SubConcernId = subconcernId ?? 0,
                        Name = masterPlans.First().Name,
                        Days = masterPlans
                            .GroupBy(x => x.DayName)
                            .Select(g => new DietDayModel
                            {
                                DayName = g.Key,
                                EmptyStomach = JsonConvert.DeserializeObject<List<string>>(g.First().EmptyStomach ?? "[]"),
                                EarlyMorningSnack = JsonConvert.DeserializeObject<List<string>>(g.First().EarlyMorningSnack ?? "[]"),
                                Exercise = JsonConvert.DeserializeObject<List<string>>(g.First().Exercise ?? "[]"),
                                Breakfast = JsonConvert.DeserializeObject<List<string>>(g.First().Breakfast ?? "[]"),
                                MidMorningSnack = JsonConvert.DeserializeObject<List<string>>(g.First().MidMorningSnack ?? "[]"),
                                Lunch = JsonConvert.DeserializeObject<List<string>>(g.First().Lunch ?? "[]"),
                                EveningSnack = JsonConvert.DeserializeObject<List<string>>(g.First().EveningSnack ?? "[]"),
                                Dinner = JsonConvert.DeserializeObject<List<string>>(g.First().Dinner ?? "[]"),
                                Bedtime = JsonConvert.DeserializeObject<List<string>>(g.First().Bedtime ?? "[]")
                            }).ToList()
                    };
                }
                else
                {
                    model = new DietPlanModel
                    {
                        UserId = id,
                        Days = new List<DietDayModel>
                        {
                            new DietDayModel { DayName = "Monday" }
                        }
                    };
                }
            }

            model ??= new DietPlanModel();

            if (model.Days == null || !model.Days.Any())
            {
                model.Days = new List<DietDayModel>
                {
                    new DietDayModel { DayName = "Monday" }
                };
            }

            model.ConcernId = concernId ?? model.ConcernId;
            model.SubConcernId = subconcernId ?? model.SubConcernId;

            ViewBag.UserId = id;
            ViewBag.UserName = user.Name;

            ViewBag.ConcernsList = _context.Concerns
                .Where(c => c.IsActive)
                .OrderBy(c => c.Name)
                .ToList();

            ViewBag.SubConcernsList = concernId.HasValue
                ? _context.SubConcerns
                    .Where(sc => sc.IsActive && sc.ConcernId == concernId.Value)
                    .OrderBy(sc => sc.Name)
                    .ToList()
                : new List<SubConcerns>();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveAssignedDietPlan([FromBody] DietPlanModel model)
        {
            if (model == null || model.Days == null || !model.Days.Any())
                return Json(new { success = false });

            var jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(model);

            var oldPlans = _context.UserDietPlans
                .Where(x => x.UserId == model.UserId && x.UserConcernId == model.ConcernId)
                .ToList();

            foreach (var p in oldPlans)
            {
                p.IsLatest = false;
            }

            int newVersion = oldPlans.Any()
                ? oldPlans.Max(x => x.Version) + 1
                : 1;

            var newPlan = new ManvarFitness.Entity.UserDietPlans
            {
                UserId = model.UserId,
                UserConcernId = model.ConcernId,

                DietPlanName = model.Name,
                DietPlanData = jsonData,

                Version = newVersion,
                IsLatest = true,
                IsActive = true,

                ValidTill = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(30))
            };

           
            try
            {
                var user = _context.Users.FirstOrDefault(x => x.UserId == model.UserId);
                var pdfModel = new DietPlanPdfModel
                {
                    UserName = user?.Name,
                    PlanName = model.Name,
                    Days = model.Days ?? new List<DietDayModel>()
                };

                var pdfResult = new ViewAsPdf("DownloadDietPlan", pdfModel)
                {
                    PageOrientation = Orientation.Landscape,
                    PageSize = Size.A4,
                    CustomSwitches = "--encoding utf-8 " +
                 "--disable-smart-shrinking " +
                 "--print-media-type " +
                 "--no-outline " +
                 "--disable-javascript " +
                 "--disable-plugins " +
                 "--quiet"
                };

                var pdfBytesTask = pdfResult.BuildFile(ControllerContext);
                pdfBytesTask.Wait();
                var pdfBytes = pdfBytesTask.Result;

                var pdfDir = Path.Combine(_env.WebRootPath, "pdfs");
                Directory.CreateDirectory(pdfDir);

                var fileName = $"DietPlan_{model.UserId}_{model.ConcernId}_{DateTime.UtcNow:yyyyMMddHHmmss}.pdf";
                var fullPath = Path.Combine(pdfDir, fileName);

                System.IO.File.WriteAllBytes(fullPath, pdfBytes);

                newPlan.PdfUrl = "/pdfs/" + fileName;
            }
            catch (Exception ex)
            {
                newPlan.PdfUrl = null;
            }

            _context.UserDietPlans.Add(newPlan);
            _context.SaveChanges();

            return Json(new { success = true, pdfUrl = newPlan.PdfUrl });
        }

        [HttpGet]
        public IActionResult DownloadDietPlan(Guid userId, int? concernId)
        {
            var latestPlan = _context.UserDietPlans
                .Where(x => x.UserId == userId && x.UserConcernId == concernId && x.IsLatest)
                .OrderByDescending(x => x.Version)
                .FirstOrDefault();

            if (latestPlan == null)
                return NotFound();

            if (!string.IsNullOrEmpty(latestPlan.PdfUrl))
            {
                var filePath = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                    latestPlan.PdfUrl.TrimStart('/')
                );

                if (System.IO.File.Exists(filePath))
                {
                    var bytes = System.IO.File.ReadAllBytes(filePath);
                    return File(bytes, "application/pdf", Path.GetFileName(filePath));
                }
            }

            var model = JsonConvert.DeserializeObject<DietPlanModel>(latestPlan.DietPlanData);

            var user = _context.Users.FirstOrDefault(x => x.UserId == userId);

            var pdfModel = new DietPlanPdfModel
            {
                UserName = user?.Name,
                PlanName = latestPlan.DietPlanName,
                Days = model?.Days ?? new List<DietDayModel>()
            };

            return new ViewAsPdf(pdfModel)
            {
                FileName = $"DietPlan_{user?.Name ?? "User"}.pdf",
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Landscape,
                PageSize = Rotativa.AspNetCore.Options.Size.A4
            };
        }

    }
}
