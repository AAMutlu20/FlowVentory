using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using FlowVentoryApp.Models;
using FlowVentoryApp.Models.ViewModels;

public class AccountController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }
    
    // Create View: Register
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }
    
    // Register Function
    [HttpPost]
    public async Task<IActionResult> Register(RegisterVM model)
    {
        if (ModelState.IsValid)
        {
            var user = new IdentityUser { UserName = model.Username, Email = model.Email};
            
            // Check if username is taken
            var userNameExists = await _userManager.FindByNameAsync(model.Username);
            if (userNameExists != null)
            {
                ModelState.AddModelError("Username", "This username is already taken. Please choose a different username.");
                return View(model);
            }

            // Check if email is taken
            var emailExists = await _userManager.FindByEmailAsync(model.Email);
            if (emailExists != null)
            {
                ModelState.AddModelError("Email", "This email is already in use. Please use a different email.");
                return View(model);
            }
            
            // Create Account
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Home");
            }
            
            // If failed, show Error
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        return View(model);
    }
    
    // Create View: Login
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }
    
    // Login Function
    [HttpPost]
    public async Task<IActionResult> Login(LoginVM model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "This account doesn't exist.");
            }
            else
            {
                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    return LocalRedirect(Url.Action("Index", "Home"));
                }

                ModelState.AddModelError(string.Empty, "You have made an error in inputting Username/Password.");
            }
        }
        return View(model);
    }
    
    
    // Logout Function
    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}