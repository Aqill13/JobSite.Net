using BusinessLayer.Abstract;
using EntityLayer.Entities;
using JobSite.Areas.Admin.Controllers;
using JobSite.Areas.User.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JobSite.Areas.User.Controllers
{
    [Area("User")]
    public class AccountController : Controller
    {
        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserService _userService;

        public AccountController(UserManager<Users> userManager, SignInManager<Users> signInManager,
            IUserService userService, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService;
            _roleManager = roleManager;
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

        // Register 

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel data)
        {
            if (!ModelState.IsValid)
            {
                return View(data);
            }

            var existingUser = await _userManager.FindByEmailAsync(data.Email);
            if (existingUser != null)
            {
                ModelState.AddModelError("", "Şəxsi emailinizi daxil edin!");
                return View(data);
            }

            Random random = new Random();
            int verificationCode = random.Next(1000, 9999);

            int uniqueNumber = random.Next(1000, 9999);

            var user = new Users
            {
                Fullname = $"{data.Firstname} {data.Lastname}",
                Email = data.Email,
                UserName = data.Username,
                ControleCode = verificationCode,
                CreateAccount = DateTime.Now,
                UniqueNumber = uniqueNumber,
                EmailConfirmed = true,
                Status = "Offline"
            };
            var create = await _userManager.CreateAsync(user, data.Password);

            if (create.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "user");
                return RedirectToAction(nameof(Login), "Account", new { area = "User" });
            }
            else
            {
                foreach (var e in create.Errors)
                {
                    ModelState.AddModelError("", e.Description);
                }
            }
            return View(data);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
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
                var login = await _signInManager.PasswordSignInAsync(user, data.Password, false, true);
                if (login.Succeeded)
                {
                    user.LastLogin = DateTime.Now;
                    user.Status = "Online";
                    await _userManager.UpdateAsync(user);
                    return RedirectToAction("Index", "Home", new { area = "" });
                }
                else if (login.IsLockedOut)
                {
                    var lockoutEndDate = await _userManager.GetLockoutEndDateAsync(user);
                    if (lockoutEndDate.HasValue)
                    {
                        var remainingTime = lockoutEndDate.Value.UtcDateTime - DateTime.UtcNow;

                        TempData["LockoutMinutes"] = Math.Ceiling(remainingTime.TotalMinutes);
                        return RedirectToAction("Locked", "Account", new { area = "User" });
                    }
                }
                else ModelState.AddModelError("", "Username or password is incorrect");
            }
            else ModelState.AddModelError("", "Username or password is incorrect");
            return View(data);
        }

        public IActionResult Locked()
        {
            var lockoutMinutes = TempData["LockoutMinutes"] != null ? (double)TempData["LockoutMinutes"] : 0;
            ViewBag.LockoutMinutes = lockoutMinutes;
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
            return RedirectToAction(nameof(Login), "Account", new { area = "User" });
        }

    }
}
