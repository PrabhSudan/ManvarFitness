using ManvarFitness.Entity;
using Microsoft.AspNetCore.Mvc;

namespace ManvarFitness.Controllers;

public class AuthController : Controller
{
    private readonly IUserRepository _userRepository;
    public AuthController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    [HttpGet]
    public IActionResult ForgotPassword()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ForgotPassword(string emailUsername, string password, string confirmPassword)
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
        var user = await _userRepository.GetUserByEmailOrUsernameAsync(emailUsername);
        if (user == null)
        {
            ViewBag.Error = "User not found";
            return View();
        }
        user.Password = password;
        await _userRepository.UpdatePasswordAsync(user);
        return RedirectToAction("Login", "Auth");
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string emailUsername, string password)
    {
        if (string.IsNullOrWhiteSpace(emailUsername) ||
            string.IsNullOrWhiteSpace(password))
        {
            ViewBag.Error = "All fields are required";
            return View();
        }
        var user = await _userRepository.GetUserByEmailOrUsernameAsync(emailUsername);
        if (user == null)
        {
            ViewBag.Error = "Invalid email/username";
            return View();
        }
        if (user == null || !user.IsActive)
        {
            ViewBag.Error = "User not found or account is disabled";
            return View();
        }

        return RedirectToAction("Index", "Dashboard");
    }

    public IActionResult Logout()
    {
        return RedirectToAction("Login", "Auth");
    }
}
