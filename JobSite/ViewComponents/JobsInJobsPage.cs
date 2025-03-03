using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace JobSite.ViewComponents
{
    public class JobsInJobsPage : ViewComponent
    {
        private readonly IWorkService _workService;

        public JobsInJobsPage(IWorkService workService)
        {
            _workService = workService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var works = await _workService.SGetWorksWithIncludesAsync(new[] { "City", "Field" }, x => x.IsActive);
            return View(works); 
        }


    }
}
