using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ManvarFitness.Controllers
{
    public class UserController : Controller
    {
        private static List<dynamic> Users = new List<dynamic>()
        {
             new { Id = 1, Name = "John Doe", Email = "john@example.com", Phone = "1234567890", Plan="Premium", Status="Active"},
             new { Id = 2, Name = "Jane Smith", Email = "jane@example.com", Phone = "0987654321", Plan="Basic", Status="Active" },
             new { Id = 3, Name = "Jamie", Email = "jamie@example.com", Phone = "1234567890", Plan="Basic", Status="Inactive" },
             new { Id = 4, Name = "Aaron", Email = "aaron@example.com", Phone = "0987654321", Plan="Premium", Status="Inactive" }
        };
    

        // GET: UserController
        public ActionResult Index()
        {
            return View(Users);
        }

        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            var user = Users.FirstOrDefault(u => u.Id == id);
            if(user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string Name, string Email, string Phone)
        {
            var user = Users.FirstOrDefault(u => u.Email == Email);
            if (user != null)
            {
                ModelState.AddModelError("Email", "Email already exists.");
                return View();
            }
            return RedirectToAction("Index");
        }

        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            var user = Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, string Name, string Email, string Phone)
        {
            var user = Users.Find(u => u.Id == id);
            if(user ==null)
            {
                return NotFound();
            }
            Users.Remove(user);
            Users.Add(new { Id = id, Name = Name, Email = Email, Phone = Phone });

            return RedirectToAction("Index");
        }

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            var user = Users.FirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound();
            return View(user);
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            var user = Users.FirstOrDefault(u => u.Id == id);
            if (user != null) Users.Remove(user);
            return RedirectToAction("Index");
        }
    }
}
