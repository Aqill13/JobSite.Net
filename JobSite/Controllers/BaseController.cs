using BusinessLayer.Abstract;
using EntityLayer.Entities;
using JobSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace JobSite.Controllers
{
    public abstract class BaseController : Controller
    {
        private readonly IFieldService _fieldService;
        private readonly ICityService _cityService;
        private readonly ICategoryService _categoryService;

        public BaseController(IFieldService fieldService, ICityService cityService, ICategoryService categoryService)
        {
            _fieldService = fieldService;
            _cityService = cityService;
            _categoryService = categoryService;
        }

        protected async Task<List<Field>> GetFieldsAsync()
        {
            return await _fieldService.SGetAllAsync(x => x.IsActive);
        }
        protected async Task<List<City>> GetCitiesAsync()
        {
            return await _cityService.SGetAllAsync(x => x.IsActive);
        }
        protected async Task<JsonResult> GetCategoriesAsync(int id)
        {
            var categories = await _categoryService.SGetAllAsync(x => x.FieldId == id && x.IsActive);
            return Json(categories);
        }

        protected async Task SetDropdownAsync()
        {
            var fields = await GetFieldsAsync();
            var cities = await GetCitiesAsync();
            ViewBag.Fields = new SelectList(fields, "Id", "Name");
            ViewBag.Cities = new SelectList(cities, "Id", "Name");
        }
    }
}
