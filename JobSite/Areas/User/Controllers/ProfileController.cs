using BusinessLayer.Abstract;
using EntityLayer.Entities;
using JobSite.Areas.User.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace JobSite.Areas.User.Controllers
{
    [Area("User")]
    public class ProfileController : Controller
    {
        private readonly IUserService _userService;
        private readonly SignInManager<Users> _signInManager;
        private readonly ICandidateService _candidateService;
        private readonly ICityService _cityService;
        private readonly IFieldService _fieldService;
        private readonly ICategoryService _categoryService;
        private readonly UserManager<Users> _userManager;

        public ProfileController(IUserService userService, UserManager<Users> userManager, ICandidateService candidateService,
            ICityService cityService, IFieldService fieldService, ICategoryService categoryService, SignInManager<Users> signInManager)
        {
            _userService = userService;
            _userManager = userManager;
            _candidateService = candidateService;
            _cityService = cityService;
            _fieldService = fieldService;
            _categoryService = categoryService;
            _signInManager = signInManager;
        }

        private async Task<List<Field>> GetFields()
        {
            return await _fieldService.SGetAllAsync(x => x.IsActive);
        }
        private async Task<List<City>> GetCities()
        {
            return await _cityService.SGetAllAsync(x => x.IsActive);
        }
        public async Task<JsonResult> GetCategories(int id)
        {
            var categories = await _categoryService.SGetAllAsync(x => x.FieldId == id && x.IsActive);
            return Json(categories);
        }

        private async Task Dropdown()
        {
            var fields = await GetFields();
            var cities = await GetCities();
            ViewBag.Fields = new SelectList(fields, "Id", "Name");
            ViewBag.Cities = new SelectList(cities, "Id", "Name");
        }
        private async Task Dropdown(Candidate candidate)
        {
            var fields = await GetFields();
            var cities = await GetCities();
            var categories = await _categoryService.SGetAllAsync(x => x.FieldId == candidate.FieldId);
            ViewBag.Fields = new SelectList(fields, "Id", "Name");
            ViewBag.Cities = new SelectList(cities, "Id", "Name");
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
        }

        //Profil (Index)

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userService.GetUserAsync();
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(Users data)
        {
            var currentUser = await _userService.GetUserAsync();
            currentUser.Fullname = data.Fullname;

            var result = await _userManager.UpdateAsync(currentUser);
            if (result.Succeeded)
            {
                TempData["updatedSuccessfully"] = "Məlumatlar uğurla yeniləndi";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(data);
        }

        //Add Cv 

        [HttpGet]
        public async Task<IActionResult> AddCv()
        {
            ViewBag.userId = _userService.GetUserId();
            await Dropdown();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCv(Candidate data, IFormFile? pImage)
        {
            if (ModelState.IsValid)
            {
                if (pImage != null)
                {
                    var extension = Path.GetExtension(pImage.FileName);
                    var uniqueImgName = Guid.NewGuid().ToString() + extension;
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/pImage/", uniqueImgName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await pImage.CopyToAsync(stream);
                    }
                    data.Image = "/pImage/" + uniqueImgName;
                }
                else data.Image = "/pImage/defaultUserImage.png";

                data.IsActive = true;
                data.PublishDate = DateTime.Now;
                data.EndDate = DateTime.Now.AddDays(30);
                await _candidateService.SCreateAsync(data);
                TempData["createdSuccessfully"] = "CV uğurla əlavə edildi";
                return RedirectToAction(nameof(MyAnnouncement));
            }
            ViewBag.userId = _userService.GetUserId();
            await Dropdown(data);
            return View(data);
        }

        //Update CV 

        [HttpGet]
        public async Task<IActionResult> UpdateCV(int id)
        {
            var upItem = await _candidateService.SReadAsync(id);
            ViewBag.ExistingImageUrl = upItem.Image;
            await Dropdown(upItem);
            return View(upItem);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCV(Candidate upData, IFormFile? pImage)
        {
            if (ModelState.IsValid)
            {
                if (pImage != null)
                {
                    var extension = Path.GetExtension(pImage.FileName);
                    var uniqueImgName = Guid.NewGuid().ToString() + extension;
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/pImage/", uniqueImgName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await pImage.CopyToAsync(stream);
                    }
                    upData.Image = "/pImage/" + uniqueImgName;
                }
                else upData.Image = Request.Form["existingImageUrl"];

                upData.IsActive = true;
                await _candidateService.SUpdateAsync(upData);
                TempData["updatedSuccessfully"] = "Elan uğurla yeniləndi";
                return RedirectToAction(nameof(MyAnnouncement));
            }
            var upItem = await _candidateService.SReadAsync(upData.Id);
            ViewBag.ExistingImageUrl = upItem.Image;
            await Dropdown(upData);
            return View(upData);
        }

        //MyAnnouncement
        public async Task<IActionResult> MyAnnouncement()
        {
            var user = await _userManager.Users.Include(x => x.Candidates).Include(x => x.Works)
                .FirstOrDefaultAsync(x => x.Id == _userService.GetUserId());
            return View(user);
        }

        //Delete announcement

        [HttpDelete]
        public async Task<JsonResult> DeleteAnnouncement(int id)
        {
            var dltItem = await _candidateService.SReadAsync(id);
            await _candidateService.SDeleteAsync(dltItem);
            return Json(new { success = true, message = "Elan uğurla silindi" });
        }

        //SecuritySettings

        [HttpGet]
        public IActionResult SecuritySettings()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SecuritySettings(ChangePasswordViewModel data)
        {
            var user = await _userManager.GetUserAsync(User);
            if (!ModelState.IsValid || user == null)
            {
                return View(data);
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, data.CurrentPassword);
            if (!isPasswordValid)
            {
                ModelState.AddModelError("", "Mövcud şifrə yanlışdır");
            }

            var changePassword = await _userManager.ChangePasswordAsync(user, data.CurrentPassword, data.NewPassword);
            if (changePassword.Succeeded)
            {
                await _signInManager.RefreshSignInAsync(user);
                TempData["SuccessMessage"] = "Şifrəniz uğurla güncəlləndi";
                return RedirectToAction("SecuritySettings");
            }
            return View(data);
        }


    }
}
