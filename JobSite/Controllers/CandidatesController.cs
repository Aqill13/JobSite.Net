using BusinessLayer.Abstract;
using DataAccessLayer.Data;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobSite.Controllers
{
    public class CandidatesController : BaseController
    {
        private readonly ICandidateService _candidateService;
        private readonly ApplicationDbContext _context;

        public CandidatesController(ICategoryService categoryService, IFieldService fieldService,
            ICityService cityService, ICandidateService candidateService, ApplicationDbContext applicationDbContext)
            : base (fieldService, cityService, categoryService)
        {
            _candidateService = candidateService;
            _context = applicationDbContext;
        }
        public async Task<JsonResult> GetCategories(int id)
        {
            return await GetCategoriesAsync(id);
        }
        private async Task SetDropdown()
        {
            await SetDropdownAsync();
        }

        public async Task<IActionResult> Index()
        {
            var candidates = _context.Candidates.Where(x =>x.IsActive).Include(x=>x.City).ToList();
            await SetDropdown();
            return View(candidates);    
        }

        [HttpGet]
        public async Task<IActionResult> Filtr(int? FieldId, int? CategoryId, int? CityId, string? Education, string? WorkExprience, int? MaxSalary)
        {
            IQueryable<Candidate> query = _context.Candidates;
            if (FieldId.HasValue)
                query = query.Where(x => x.FieldId == FieldId);
            if (CategoryId.HasValue)
                query = query.Where(x=>x.CategoryId == CategoryId);
            if (CityId.HasValue)
                query = query.Where(x=>x.CityId == CityId);
            if (!string.IsNullOrWhiteSpace(Education))
                query = query.Where(x => x.Education == Education);
            if (!string.IsNullOrWhiteSpace(WorkExprience))
                query = query.Where(x => x.WorkExperience == WorkExprience);
            if (MaxSalary.HasValue)
                query = query.Where(x => x.MinSalary <= MaxSalary);

            var result = query.Select(x => new
            {
                x.Id,
                x.Position,
                x.Firstname,
                x.Lastname,
                x.MinSalary,
                x.Age,
                x.Image,
                x.WorkExperience,
                x.Education,
                cityName = x.City.Name
            }).ToList();
            return Json(result);
        }

        public IActionResult Details(int id)
        {
            var item = _context.Candidates.Where(x=>x.Id==id).Include(x=>x.City).Include(x=>x.Field).Include(x=>x.Category).FirstOrDefault();
            return View(item);
        }
    }
}
