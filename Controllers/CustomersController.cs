using ManvarFitness.Database;
using ManvarFitness.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ManvarFitness.Controllers
{
    public class CustomersController : BaseController
    {
        private readonly ApplicationDbContext _context;
        public CustomersController(ApplicationDbContext context): base(context)
        {
            _context = context;
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
    }
}
