using Microsoft.AspNetCore.Mvc;

namespace ManvarFitness.Controllers;

public class AuthController : Controller
{
    [HttpGet]
    public IActionResult ForgotPassword()
    {
        return View();
    }

    [HttpPost]
    public IActionResult ForgotPassword(string emailUsername, string password, string confirmPassword)
    {
        if (string.IsNullOrWhiteSpace(emailUsername) ||
            string.IsNullOrWhiteSpace(password) ||
            string.IsNullOrWhiteSpace(confirmPassword))
        {
            ViewBag.Error = "All fields are required";
            return View();
        }

        if (password != confirmPassword)
        {
            ViewBag.Error = "Passwords do not match";
            return View();
        }

        return RedirectToAction("Login", "Auth");
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(string emailUsername, string password)
    {
        if (string.IsNullOrWhiteSpace(emailUsername) ||
            string.IsNullOrWhiteSpace(password))
        {
            ViewBag.Error = "All fields are required";
            return View();
        }
        return RedirectToAction("Index", "Dashboard");
    }

    public IActionResult Logout()
    {
        return RedirectToAction("Login", "Auth");
    }
}
