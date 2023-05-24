using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RabbitRegister.Services.TrimmingService;

namespace RabbitRegister.Pages.Main.Trimming
{
    [Authorize(Policy = "BreederOnly")]
    public class EditTrimmingModel : PageModel
    {
        private ITrimmingService _trimmingService;
        public EditTrimmingModel(ITrimmingService trimmingService)
        {
            _trimmingService = trimmingService;
        }
        [BindProperty]
        public Model.Trimming Trimming { get; set; }
		[FromQuery(Name = "Owner")]
		public int OwnerId { get; set; }
		public IActionResult OnGet(int id)
        {
            Trimming = _trimmingService.GetTrimming(id);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _trimmingService.UpdateTrimming(Trimming, id);
			return RedirectToPage($"GetAllTrimming", new { Owner = OwnerId });
		}
    }
}
