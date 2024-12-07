using CodeSimits.Models;
using CodeSimits.Services.Interfaces;
using CodeSimits.ViewModels.Auths;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace CodeSimits.Areas.Admin.Controllers;

[Area("Admin")]
public class AccountController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IEmailService _emailService;
    public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IEmailService emailService, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _emailService = emailService;
        _roleManager = roleManager;
    }

    public IActionResult Login()
    {
        return View();
    }

    public async Task<IActionResult> Register()
    {
        ViewBag.Roles = await _roleManager.Roles.ToListAsync();
        return View();
    }

    public async Task<IActionResult> VerifyEmail(string token, string user)
    {
        AppUser? userEnt = await _userManager.FindByNameAsync(user);
        if (userEnt == null) return BadRequest();

        token = token.Replace(' ', '+');
        var res = await _userManager.ConfirmEmailAsync(userEnt, token);
        if (!res.Succeeded)
        {
            foreach (var err in res.Errors)
            {
                ModelState.AddModelError("", err.Description);
            }
        }

        await _signInManager.SignInAsync(userEnt, true);
        return RedirectToAction("Index", "Home");
    }

    [Authorize(Roles = "Tyutor, Admin")]
    [HttpPost]
    public async Task<IActionResult> Register(RegisterVM vm)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        AppUser user = new AppUser
        {
            UserName = vm.Username,
            Email = vm.EmailAddress,
        };

        var res = await _userManager.CreateAsync(user, vm.Password);
        if (!res.Succeeded)
        {
            foreach (var err in res.Errors)
            {
                ModelState.AddModelError("", err.Description);
            }
            return View();
        }
        IdentityRole? roleStr = await _roleManager.FindByIdAsync(vm.Role);
        if (roleStr == null)
        {
            return BadRequest();
        }

        var roleRes = await _userManager.AddToRoleAsync(user, roleStr.Name);

        if (!roleRes.Succeeded)
        {
            foreach (var err in roleRes.Errors)
            {
                ModelState.AddModelError("", err.Description);
            }
            return View();
        }

        string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        await _emailService.SendEmailConfirmationAsync(user.Email, user.UserName, vm.Password, token);
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginVM vm, string? returnUrl)
    {
        if (User.Identity?.IsAuthenticated ?? false)
        {
            return RedirectToAction("Index", "Home");
        }

        if (!ModelState.IsValid)
        {
            return View();
        }


        AppUser? user = await _userManager.FindByNameAsync(vm.Username);
        if (user == null)
        {
            ModelState.AddModelError("", "Username or password is wrong");
            return View();
        }

        if (!user.EmailConfirmed)
        {
            ModelState.AddModelError("", "You need to confirm you email!");
            return View();
        }

        var res = await _signInManager.PasswordSignInAsync(user, vm.Password, vm.RememberMe, true);

        if (!res.Succeeded)
        {
            if (res.IsNotAllowed)
            {
                ModelState.AddModelError("", "You do not have permission");
            }
            else if (res.IsLockedOut)
            {
                ModelState.AddModelError("", $"Wait until {user.LockoutEnd!.Value}");
            }
            else
            {
                ModelState.AddModelError("", "Username or password is wrong");
            }
            return View();
        }

        if (!returnUrl.IsNullOrEmpty())
        {
            return Redirect(returnUrl!);
        }

        if (HttpContext.User?.FindFirst(ClaimTypes.Role)?.Value.ToLower() == "admin")
        {
            return RedirectToAction("Index", new { Controller = "Dashboard", Area = "Admin" });
        }

        return RedirectToAction("Index", "Home");
    }
}
