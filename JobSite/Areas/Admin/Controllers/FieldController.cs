using BusinessLayer.Abstract;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace JobSite.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminPolicy")]
    public class FieldController : Controller
    {
        private readonly IFieldService _fieldService;
        private readonly ICategoryService _categoryService;
        private readonly IWorkService _workService;

        public FieldController(IFieldService fieldService, ICategoryService categoryService, IWorkService workService)
        {
            _fieldService = fieldService;
            _categoryService = categoryService;
            _workService = workService;
        }

        public async Task<IActionResult> Index()
        {
            var fieldList = await _fieldService.SGetAllAsync();
            return View(fieldList);
        }

        //Create

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Field data)
        {
            if (ModelState.IsValid)
            {
                data.CreatedDate = DateTime.Now;
                data.IsActive = true;
                await _fieldService.SCreateAsync(data);
                TempData["createdSuccessfully"] = "Created successfully";
                return RedirectToAction(nameof(Index), "Field", new { area = "Admin" });
            }
            return View(data);
        }

        //Update

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var updateItem = await _fieldService.SReadAsync(id);
            return View(updateItem);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Field upData)
        {
            if (ModelState.IsValid)
            {
                var categories = await _categoryService.SGetAllAsync(x => x.FieldId == upData.Id);
                foreach (var c in categories)
                {
                    c.IsActive = upData.IsActive;
                    await _categoryService.SUpdateAsync(c);
                }

                var works = await _workService.SGetAllAsync(x => x.FieldId == upData.Id);
                foreach (var w in works)
                {
                    w.IsActive = upData.IsActive;
                    await _workService.SUpdateAsync(w);
                }

                await _fieldService.SUpdateAsync(upData);
                TempData["updatedSuccessfully"] = "Updated successfully";
                return RedirectToAction(nameof(Index), "Field", new { area = "Admin" });
            }
            return View(upData);
        }

        //Delete

        [HttpDelete]
        public async Task<JsonResult> Delete(int id)
        {
            var deletedItem = await _fieldService.SReadAsync(id);
            await _fieldService.SDeleteAsync(deletedItem);
            return Json(new { success = true, message = "Deleted successfully" });
        }

        //Field details

        public async Task<JsonResult> FieldInfo(int id)
        {
            var field = await _fieldService.SGetFieldWithCategoriesWithWorksAsync(id);
            var categoriesByFieldId = await _categoryService.SGetAllAsync(x => x.FieldId == id);

            var data = new
            {
                fieldName = field.Name,
                categories = field.Categories.Select(x => x.Name).ToList(),
                totalJobsCount = field.Works.Count,
                jobsCount = categoriesByFieldId.Select(c => new
                {
                    jobCount = c.Works.Count
                })
            };
            return Json(data);
        }
    }
}
