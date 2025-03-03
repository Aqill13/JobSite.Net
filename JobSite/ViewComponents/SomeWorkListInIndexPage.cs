using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace JobSite.ViewComponents
{
    public class SomeWorkListInIndexPage : ViewComponent
    {
        private readonly IWorkService _workService;

        public SomeWorkListInIndexPage(IWorkService workService)
        {
            _workService = workService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var works = await _workService.SGetWorksWithIncludesAsync(new[] { "City" }, x => x.IsActive);
            return View(works.OrderByDescending(x => x.PublishDate).Take(10).ToList());
        }
    }
}
