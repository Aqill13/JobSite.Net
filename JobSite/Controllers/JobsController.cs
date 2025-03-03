using BusinessLayer.Abstract;
using DataAccessLayer.Data;
using EntityLayer.Entities;
using JobSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace JobSite.Controllers
{
    public class JobsController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly IWorkService _workService;

        public JobsController(ICategoryService categoryService, IFieldService fieldService,
            ICityService cityService, IWorkService workService, ApplicationDbContext context)
            : base(fieldService, cityService, categoryService)
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


        [HttpGet]
        public IActionResult Filtr(int? FieldId, int? CategoryId, int? CityId, string? Education, string? WorkExprience, int? MinSalary)
        {
            IQueryable<Work> query = _context.Works;

            if (FieldId.HasValue)
                query = query.Where(x => x.FieldId == FieldId);
            if (CategoryId.HasValue)
                query = query.Where(x => x.CategoryId == CategoryId);
            if (CityId.HasValue)
                query = query.Where(x => x.CityId == CityId);
            if (WorkExprience != null)
                query = query.Where(x => x.WorkExperience == WorkExprience);
            if (Education != null)
                query = query.Where(x => x.Education == Education);
            if (MinSalary != null)
                query = query.Where(x => x.Salary >= MinSalary);

            var result = query.Select(x => new
            {
                x.Id,
                x.Position,
                x.Salary,
                x.ModeOfWork,
                x.IsNegotiable,
                fieldName = x.Field.Name,
                fieldIcon = x.Field.Icon,
                cityName = x.City.Name
            }).ToList();

            return Json(result);
        }

        public async Task<IActionResult> Index()
        {
            await SetDropdown();
            return View();
        }

        public IActionResult JobDetails(int id)
        {
            var work = _context.Works
                .Include(x => x.City).Include(x => x.Field).Include(x => x.Category)
                .Where(x => x.Id == id).FirstOrDefault();
            return View(work);
        }

    }
}
