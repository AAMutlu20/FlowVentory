using FlowVentory.BLL.Services;
using Microsoft.AspNetCore.Mvc;

public class AuthController : Controller
{
    private readonly AuthenticationService _authenticationService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthController(AuthenticationService authenticationService, IHttpContextAccessor httpContextAccessor)
    {
        _authenticationService = authenticationService;
        _httpContextAccessor = httpContextAccessor;
    }

    [HttpPost]
    public IActionResult Login(string username, string password)
    {
        if (_authenticationService.Login(username, password))
        {
            var user = _authenticationService.GetUserByUsername(username);
            _httpContextAccessor.HttpContext.Session.SetString("Username", user.Username);
            _httpContextAccessor.HttpContext.Session.SetString("Email", user.Email);
            return RedirectToAction("Index", "Home");
        }
        else
        {
            ModelState.AddModelError("", "Invalid username or password");
        }
        return View();
    }

    [HttpPost]
    public IActionResult Register(string username, string password, string email)
    {
        if (_authenticationService.Register(username, password, email))
        {
            return RedirectToAction("Login", "Auth");
        }
        else
        {
            ModelState.AddModelError("", "Username already exists");
        }
        return View();
    }

    [HttpPost]
    public IActionResult Logout()
    {
        _httpContextAccessor.HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");
    }
}