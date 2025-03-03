using BusinessLayer.Abstract;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace JobSite.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminPolicy")]
    public class CandidateController : Controller
    {
        private readonly ICandidateService _candidateService;
        private readonly ICityService _cityService;
        private readonly IFieldService _fieldService;
        private readonly ICategoryService _categoryService;
        private readonly IUserService _userService;

        public CandidateController(ICandidateService candidateService, 
            ICityService cityService, IFieldService fieldService, ICategoryService categoryService, IUserService userService)
        {
            _candidateService = candidateService;
            _cityService = cityService;
            _fieldService = fieldService;
            _categoryService = categoryService;
            _userService = userService;
        }

        private async Task<List<Field>> GetFields()
        {
            var fields = await _fieldService.SGetAllAsync(x => x.IsActive);
            return fields;
        }
        private async Task<List<City>> GetCities()
        {
            var cities = await _cityService.SGetAllAsync(x => x.IsActive);
            return cities;
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

        public async Task<JsonResult> GetCandidateInfo(int wId, string type)
        {
            var candidate = await _candidateService.SReadAsync(wId);
            var data = type switch
            {
                "EducationMore" => candidate.EducationMore,
                "WorkExperienceMore" => candidate.WorkExperienceMore,
                "Skills" => candidate.Skills,
                "AddInformation" => candidate.AddInformation,
                _ => string.Empty
            };
            return Json(new
            {
                fullName = candidate.Firstname + " " + candidate.Lastname + " " + candidate.Patronymic,
                data
            });
        }

        //Index

        public async Task<IActionResult> Index()
        {
            var candidates = await _candidateService.SGetCandidatesWithIncludesAsync(new[] { "Field", "City", "User", "Category" });
            return View(candidates);
        }

        //Create 

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.userId = _userService.GetUserId();
            await Dropdown();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Candidate data, IFormFile? pImage)
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
                TempData["createdSuccessfully"] = "Created successfully";
                return RedirectToAction(nameof(Index), "Candidate", new { area = "Admin" });
            }
            ViewBag.userId = _userService.GetUserId();
            await Dropdown(data);
            return View(data);
        }

        //Update

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var upItem = await _candidateService.SReadAsync(id);
            ViewBag.ExistingImageUrl = upItem.Image;
            await Dropdown(upItem);
            return View(upItem);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Candidate upData, IFormFile? pImage)
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

                //upData.IsActive = true;
                await _candidateService.SUpdateAsync(upData);
                TempData["updatedSuccessfully"] = "Updated successfully";
                return RedirectToAction(nameof(Index), "Candidate", new { area = "Admin" });
            }
            var upItem = await _candidateService.SReadAsync(upData.Id);
            ViewBag.ExistingImageUrl = upItem.Image;
            await Dropdown(upData);
            return View(upData);
        }

        //Delete

        [HttpDelete]
        public async Task<JsonResult> Delete(int id)
        {
            var deleteItem = await _candidateService.SReadAsync(id);
            await _candidateService.SDeleteAsync(deleteItem);
            return Json(new { success = true, message = "Deleted successfully" });
        }
    }
}
