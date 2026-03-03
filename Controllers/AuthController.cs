using ManvarFitness.Interface;
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
    public async Task<IActionResult> ForgotPassword(string email, string password, string confirmPassword)
    {
        if (string.IsNullOrWhiteSpace(email) ||
            string.IsNullOrWhiteSpace(password) ||
            string.IsNullOrWhiteSpace(confirmPassword))
        {
            ViewBag.Error = "All fields are required";
            return View();
        }

        else if (password != confirmPassword)
        {
            ViewBag.Error = "Passwords do not match";
            return View();
        }
        var user = await _userRepository.GetUserByEmailOrUsernameAsync(email);
        if  (user == null)
        {
            ViewBag.Error = "User not found";
            return View();
        }
        else
        {
            user.Password = password;
            await _userRepository.UpdatePasswordAsync(user);
        }
            
        return RedirectToAction("Login", "Auth");
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string email, string password)
    {
        var user = await _userRepository.GetUserByEmailOrUsernameAsync(email);
        if (string.IsNullOrWhiteSpace(email) ||
            string.IsNullOrWhiteSpace(password))
        {
            ViewBag.Error = "All fields are required";
            return View();
        }
        
        else if (user == null)
        {
            ViewBag.Error = "Invalid email/username";
            return View();
        }
        else if (string.IsNullOrEmpty(password))
        {
            ViewBag.Error = "Password is required";
            return View();
        }
        else if (user.Password != password)
        {
            ViewBag.Error = "Invalid Password";
            return View();
        }
        else if (user == null || !user.IsActive)
        {
            ViewBag.Error = "User not found or account is disabled";
            return View();
        }
        else
        {
            HttpContext.Session.SetString("Email", user.Email);
            HttpContext.Session.SetString("UserRole", user.Role);
            HttpContext.Session.SetString("UserId", user.Id.ToString());
            HttpContext.Session.SetString("Name", user.Name);
        }
        return RedirectToAction("Index", "Dashboard");
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login", "Auth");
    }
}
