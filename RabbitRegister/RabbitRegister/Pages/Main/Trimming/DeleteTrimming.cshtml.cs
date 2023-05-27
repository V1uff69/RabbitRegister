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
    public class DeleteTrimmingModel : PageModel
    {
        /// <summary>
        /// TrimmingService instance
        /// </summary>
        private ITrimmingService _trimmingService;

        /// <summary>
        /// default constructor for DeleteTrimmingModel
        /// </summary>
        /// <param name="trimmingService"></param>
        public DeleteTrimmingModel(ITrimmingService trimmingService) 
        {
            _trimmingService = trimmingService;
        }
        /// <summary>
        /// The trimming instance being deleted
        /// </summary>
        [BindProperty]
        public Model.Trimming Trimming { get; set; }
        /// <summary>
        /// Reference to the logged in user/owner
        /// </summary>
		[FromQuery(Name = "Owner")]
		public int OwnerId { get; set; }
        /// <summary>
        /// when page loads, we initialize the trimming being deleted by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
		public IActionResult OnGet(int id)
        {
            Trimming = _trimmingService.GetTrimming(id);
            return Page();
        }
        /// <summary>
        /// OnPost for deleting trimming by ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult OnPost(int id)
        {
            _trimmingService.DeleteTrimming(id);
			return RedirectToPage($"GetAllTrimming", new { Owner = OwnerId });
		}
    }
}
