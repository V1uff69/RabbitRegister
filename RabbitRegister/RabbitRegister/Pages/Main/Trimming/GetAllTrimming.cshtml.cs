using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Infrastructure;
using RabbitRegister.Model;
using RabbitRegister.Services.TrimmingService;

namespace RabbitRegister.Pages.Main.Trimming
{
    public class GetAllTrimmingModel : PageModel
    {
        private ITrimmingService _trimmingService;
        public GetAllTrimmingModel(ITrimmingService trimmingService)
        {
            _trimmingService = trimmingService;
        }
        public List<Model.Trimming>? trimmings { get; set; }

        [BindProperty]
        public string SearchString { get; set; }
        [BindProperty]
        public int SearchId { get; set; }
        public void OnGet()
        {
            trimmings = _trimmingService.GetTrimmings();
        }
        public IActionResult OnGetSortById()
        {
            trimmings = _trimmingService.SortById().ToList();
            return Page();
        }

        public IActionResult OnGetSortByIdDescending()
        {
            trimmings = _trimmingService.SortByIdDescending().ToList();
            return Page();
        }

        public IActionResult OnGetSortByRabbitId()
        {
            trimmings = _trimmingService.SortByRabbitId().ToList();
            return Page();
        }

        public IActionResult OnGetSortByRabbitIdDescending()
        {
            trimmings = _trimmingService.SortByRabbitIdDescending().ToList();
            return Page();
        }

        public IActionResult OnGetSortByDate()
        {
            trimmings = _trimmingService.SortByDate().ToList();
            return Page();
        }

        public IActionResult OnGetSortByDateDescending()
        {
            trimmings = _trimmingService.SortByDateDescending().ToList();
            return Page();
        }

        public IActionResult OnPostNameSearch()
        {
            trimmings = _trimmingService.NameSearch(SearchString).ToList();
            return Page();
        }

        public IActionResult OnPostRabbitIdSearch()
        {
            trimmings = _trimmingService.RabbitIdSearch(SearchId).ToList();
            return Page();
        }
    }
}
