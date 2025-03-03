using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace JobSite.ViewComponents
{
    public class CandidatesInIndexPage : ViewComponent
    {
        private readonly ICandidateService _candidateService;

        public CandidatesInIndexPage(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var candidates = await _candidateService.SGetAllAsync(x => x.IsActive);
            return View(candidates);
        }
    }
}
