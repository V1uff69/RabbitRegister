using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RabbitRegister.Services.TrimmingService;

namespace RabbitRegister.Pages.Main.Trimming
{
    /// <summary>
    /// claim role , so only breeders can see page.
    /// </summary>
    [Authorize(Policy = "BreederOnly")]
    public class EditTrimmingModel : PageModel
    {
        /// <summary>
        /// TrimmingService instance
        /// </summary>
        private ITrimmingService _trimmingService;
        /// <summary>
        /// default constructor for EditTrimmingModel
        /// </summary>
        /// <param name="trimmingService"></param>
        public EditTrimmingModel(ITrimmingService trimmingService)
        {
            _trimmingService = trimmingService;
        }
        /// <summary>
        /// The Trimming instance being edited
        /// </summary>
        [BindProperty]
        public Model.Trimming Trimming { get; set; }
        /// <summary>
        /// Reference to the logged in user/owner
        /// </summary>
		[FromQuery(Name = "Owner")]
		public int OwnerId { get; set; }
        /// <summary>
        /// when page loads, we initialize the trimming being edited by its ID 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
		public IActionResult OnGet(int id)
        {
            Trimming = _trimmingService.GetTrimming(id);
            return Page();
        }
        /// <summary>
        /// OnPost for updating trimming by ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
