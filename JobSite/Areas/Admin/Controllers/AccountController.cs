using EntityLayer.Entities;
using JobSite.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JobSite.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminPolicy")]
    public class AccountController : Controller
    {
        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signInManager;

        public AccountController(UserManager<Users> userManager, SignInManager<Users> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpPost]
        public async Task<IActionResult> KeepAlive()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user != null)
            {
                user.LastActiveTime = DateTime.Now;
                await _userManager.UpdateAsync(user);
            }
            return Ok();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel data)
        {
            if (!ModelState.IsValid)
            {
                return View(data);
            }
            var user = await _userManager.FindByNameAsync(data.Username);
            if (user != null)
            {
                var login = await _signInManager.PasswordSignInAsync(user, data.Password, data.RememberMe, true);
                if (login.Succeeded)
                {
                    user.Status = "Online";
                    await _userManager.UpdateAsync(user);
                    return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
                }
                else if (login.IsLockedOut)
                {
                    if (user.LockoutEnd.HasValue)
                    {
                        var timeLeft = user.LockoutEnd.Value - DateTimeOffset.UtcNow;
                        TempData["TimeLeft"] = timeLeft.ToString(@"hh\:mm\:ss");
                    }
                    return RedirectToAction("Lockout", "Account", new { area = "Admin" });
                }
                else
                {
                    ModelState.AddModelError("", "Username or password is incorrect");
                    return View(data);
                }
            }
            else
            {
                ModelState.AddModelError("", "Username or password is incorrect");
                return View(data);
            }
        }

        [AllowAnonymous]
        public IActionResult Lockout()
        {
            if (TempData["TimeLeft"] is string timeLeftString)
            {
                if (TimeSpan.TryParse(timeLeftString, out var timeLeft))
                {
                    ViewBag.TimeLeft = timeLeft;
                }
            }
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user != null)
            {
                user.Status = "Offline";
                await _userManager.UpdateAsync(user);
            }
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login), "Account", new { area = "Admin" });
        }
    }
}
