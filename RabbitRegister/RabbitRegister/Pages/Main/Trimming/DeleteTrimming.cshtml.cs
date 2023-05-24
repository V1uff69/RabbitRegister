using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RabbitRegister.Services.TrimmingService;

namespace RabbitRegister.Pages.Main.Trimming
{
    [Authorize(Policy = "BreederOnly")]
    public class DeleteTrimmingModel : PageModel
    {
        private ITrimmingService _trimmingService;

        public DeleteTrimmingModel(ITrimmingService trimmingService) 
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
        public IActionResult OnPost(int id)
        {
            _trimmingService.DeleteTrimming(id);
			return RedirectToPage($"GetAllTrimming", new { Owner = OwnerId });
		}
    }
}
