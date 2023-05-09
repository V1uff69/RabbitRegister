using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RabbitRegister.Services.TrimmingService;

namespace RabbitRegister.Pages.Main.Trimming
{
    public class EditTrimmingModel : PageModel
    {
        private ITrimmingService _trimmingService;
        public EditTrimmingModel(ITrimmingService trimmingService)
        {
            _trimmingService = trimmingService;
        }
        [BindProperty]
        public Model.Trimming Trimming { get; set; }
        public IActionResult OnGet(int id)
        {
            Trimming = _trimmingService.GetTrimming(id);
            return Page();
        }

        public IActionResult OnPost(int id)
        {
            _trimmingService.UpdateTrimming(Trimming, id);
            return RedirectToPage("GetAllTrimming");
        }
    }
}
