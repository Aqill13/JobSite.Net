using BusinessLayer.Abstract;
using DataAccessLayer.Data;
using EntityLayer.Entities;
using JobSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace JobSite.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly IWorkService _workService;

        public HomeController(ICategoryService categoryService, IFieldService fieldService, 
            ICityService cityService, IWorkService workService, ApplicationDbContext context)
            : base (fieldService, cityService, categoryService)
        {
            _context = context;
            _workService = workService;
        }
        public async Task<JsonResult> GetCategories(int id)
        {
            return await GetCategoriesAsync(id);
        }
        private async Task SetDropdown()
        {
            await SetDropdownAsync();
        }

        [HttpPost]
        public IActionResult Filtr(FiltrModel data)
        {
            IQueryable<Work> query = _context.Works.Include(x => x.Field).Include(x => x.City);

            if (data.FieldId.HasValue)
                query = query.Where(x => x.FieldId == data.FieldId);
            if (data.CategoryId.HasValue)
                query = query.Where(x => x.CategoryId == data.CategoryId);
            if (data.CityId.HasValue)
                query = query.Where(x => x.CityId == data.CityId);
            if (data.WorkExprience != null)
                query = query.Where(x => x.WorkExperience == data.WorkExprience);
            if (data.Education != null)
                query = query.Where(x => x.Education == data.Education);
            if (data.MinSalary != null)
                query = query.Where(x => x.Salary >= data.MinSalary);

            var result = query.ToList();
            return View(result);
        }

        public async Task<IActionResult> WorksInFieldId(int id)
        {
            var worksInFieldId = await _workService.SGetWorksWithIncludesAsync(new[] { "City", "Field" }, x => x.FieldId == id);
            var field = _context.Fields.Where(x => x.Id == id).First();
            ViewBag.FieldName = field.Name;
            return View(worksInFieldId);
        }

        public async Task<IActionResult> Index()
        {
            await SetDropdown();
            return View();
        }
    }
}
