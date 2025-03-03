using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace JobSite.ViewComponents
{
    public class CategoriesInIndexPage : ViewComponent
    {
        private readonly IFieldService _fieldService;

        public CategoriesInIndexPage(IFieldService fieldService)
        {
            _fieldService = fieldService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var fields = await _fieldService.SGetAllWithWorksAsync(x => x.IsActive);
            return View(fields);
        }
    }
}
