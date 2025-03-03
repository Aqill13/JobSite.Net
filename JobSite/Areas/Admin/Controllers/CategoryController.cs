using BusinessLayer.Abstract;
using DataAccessLayer.Data;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace JobSite.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminPolicy")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IFieldService _fieldService;
        private readonly IWorkService _workService;

        public CategoryController(ICategoryService categoryService, IFieldService fieldService, IWorkService workService)
        {
            _categoryService = categoryService;
            _fieldService = fieldService;
            _workService = workService;
        }

        private async Task FieldsDropdown()
        {
            var fields = await _fieldService.SGetAllAsync(x => x.IsActive);
            ViewBag.Fields = new SelectList(fields, "Id", "Name");
        }

        public async Task<IActionResult> Index()
        {
            var fieldList = await _fieldService.SGetAllAsync();
            return View(fieldList);
        }

        [HttpGet]
        public async Task<IActionResult> GetCategoriesByField(int id)
        {
            var categories = await _categoryService.SGetAllWithWorksAsync(x => x.FieldId == id);

            var data = categories.Select(c => new
            {
                Id = c.Id,
                Name = c.Name,
                IsActive = c.IsActive,
                CreatedDate = c.CreatedDate,
                WorkCount = c.Works.Count
            }).ToList();

            return Json(data);
        }


        //Create

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await FieldsDropdown();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category data)
        {
            if (ModelState.IsValid)
            {
                data.CreatedDate = DateTime.Now;
                data.IsActive = true;
                await _categoryService.SCreateAsync(data);
                TempData["createdSuccessfully"] = "Created successfully";
                return RedirectToAction(nameof(Index), "Category", new { area = "Admin" });
            }
            await FieldsDropdown();
            return View(data);
        }

        //Update

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var updateItem = await _categoryService.SReadAsync(id);
            await FieldsDropdown();
            return View(updateItem);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Category upData)
        {
            if (ModelState.IsValid)
            {
                var works = await _workService.SGetAllAsync(x => x.CategoryId == upData.Id);
                foreach (var w in works)
                {
                    w.IsActive = upData.IsActive;
                    await _workService.SUpdateAsync(w);
                }
                await _categoryService.SUpdateAsync(upData);
                TempData["updatedSuccessfully"] = "Updated successfully";
                return RedirectToAction(nameof(Index), "Category", new { area = "Admin" });
            }
            await FieldsDropdown();
            return View(upData);
        }

        //Delete

        [HttpDelete]
        public async Task<JsonResult> Delete(int id)
        {
            var deleteItem = await _categoryService.SReadAsync(id);
            await _categoryService.SDeleteAsync(deleteItem);
            return Json(new { success = true, message = "Deleted successfully" });
        }

    }
}
