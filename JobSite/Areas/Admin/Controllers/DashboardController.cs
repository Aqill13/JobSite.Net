using BusinessLayer.Abstract;
using DataAccessLayer.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobSite.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminPolicy")]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var totalJobs = _context.Works.Count();
            ViewBag.TotalJobs = totalJobs;

            var totalCandidates = _context.Candidates.Count();
            ViewBag.TotalCandidates = totalCandidates;

            var totalFields = _context.Fields.Count();
            ViewBag.TotalFields = totalFields;

            var totalCategories = _context.Categories.Count();
            ViewBag.TotalCategories = totalCategories;

            return View();
        }
    }
}
