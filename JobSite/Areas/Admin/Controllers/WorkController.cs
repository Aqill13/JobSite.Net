using BusinessLayer.Abstract;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualBasic;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace JobSite.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminPolicy")]
    public class WorkController : Controller
    {
        private readonly IFieldService _fieldService;
        private readonly ICityService _cityService;
        private readonly ICategoryService _categoryService;
        private readonly IWorkService _workService;
        private readonly IUserService _userService;

        public WorkController(IFieldService fieldService, ICityService cityService, ICategoryService categoryService,
            IWorkService workService, IUserService userService)
        {
            _fieldService = fieldService;
            _cityService = cityService;
            _categoryService = categoryService;
            _workService = workService;
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
        private async Task Dropdown()
        {
            var fields = await GetFields();
            var cities = await GetCities();
            ViewBag.Fields = new SelectList(fields, "Id", "Name");
            ViewBag.Cities = new SelectList(cities, "Id", "Name");
        }
        private async Task Dropdown(Work work)
        {
            var fields = await GetFields();
            var cities = await GetCities();
            var categories = await _categoryService.SGetAllAsync(x => x.FieldId == work.FieldId);
            ViewBag.Fields = new SelectList(fields, "Id", "Name");
            ViewBag.Cities = new SelectList(cities, "Id", "Name");
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
        }
        public async Task<JsonResult> GetCategories(int id)
        {
            var categories = await _categoryService.SGetAllAsync(x => x.FieldId == id && x.IsActive);
            return Json(categories);
        }

        //Index
        public async Task<IActionResult> Index()
        {
            var works = await _workService.SGetWorksWithIncludesAsync(new[] { "Field", "Category", "City", "User" });
            return View(works);
        }

        //Modal Info
        public async Task<JsonResult> GetJobInfo(int wId, string type)
        {
            var work = await _workService.SReadAsync(wId);
            var data = type switch
            {
                "JobInformation" => work.JobInformation,
                "Requirements" => work.Requirements,
                "Note" => work.Note,
                _ => string.Empty
            };

            return Json(new
            {
                positionName = work.Position,
                data
            });
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
        public async Task<IActionResult> Create(Work data)
        {
            if (ModelState.IsValid)
            {
                data.IsActive = true;
                data.PublishDate = DateTime.Now;
                data.EndDate = DateTime.Now.AddDays(30);
                await _workService.SCreateAsync(data);
                TempData["createdSuccessfully"] = "Created successfully";
                return RedirectToAction(nameof(Index), "Work", new { area = "Admin" });
            }
            ViewBag.userId = _userService.GetUserId();
            await Dropdown(data);
            return View(data);
        }

        //Update

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var upItem = await _workService.SReadAsync(id);
            await Dropdown(upItem);
            return View(upItem);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Work upData)
        {
            if (ModelState.IsValid)
            {
                await _workService.SUpdateAsync(upData);
                TempData["updatedSuccessfully"] = "Updated successfully";
                return RedirectToAction(nameof(Index), "Work", new { area = "Admin" });
            }
            await Dropdown(upData);
            return View(upData);
        }

        //Delete

        [HttpDelete]
        public async Task<JsonResult> Delete(int id)
        {
            var deleteItem = await _workService.SReadAsync(id);
            await _workService.SDeleteAsync(deleteItem);
            return Json(new { success = true, message = "Deleted successfully" });
        }
    }
}
