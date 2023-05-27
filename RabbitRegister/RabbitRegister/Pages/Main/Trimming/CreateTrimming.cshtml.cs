using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RabbitRegister.Services.RabbitService;
using RabbitRegister.Services.TrimmingService;

namespace RabbitRegister.Pages.Main.Trimming
{
    /// <summary>
    /// claim role , so only breeders can see page.
    /// </summary>
    [Authorize(Policy = "BreederOnly")]
    public class CreateTrimmingModel : PageModel
    {
        /// <summary>
        /// TrimmingService instance
        /// </summary>
        private ITrimmingService _trimmingService;
        /// <summary>
        /// RabbitService instance
        /// </summary>
        private IRabbitService _rabbitService;
        /// <summary>
        /// default constructor for GetAllTrimmingModel
        /// </summary>
        /// <param name="trimmingService"></param>
        /// <param name="rabbitService"></param>
        public CreateTrimmingModel(ITrimmingService trimmingService, IRabbitService rabbitService)
        {
            _trimmingService = trimmingService;
            _rabbitService = rabbitService;
        }
        /// <summary>
        /// The trimming instance being created
        /// </summary>
        [BindProperty]
        public Model.Trimming Trimming { get; set; } = new Model.Trimming();
        /// <summary>
        /// Reference to the logged in user/owner
        /// </summary>
        [FromQuery(Name = "Owner")]
        public int OwnerId { get; set; }
        /// <summary>
        /// Loads either a empty trimming or a trimming for specific rabbit identified by RabbitRegNo and BreederRegNo
        /// </summary>
        /// <param name="RabbitRegNo"></param>
        /// <param name="BreederRegNo"></param>
        /// <returns></returns>
        public IActionResult OnGet(int RabbitRegNo, int BreederRegNo)
        {
            if (RabbitRegNo > 0 && BreederRegNo > 0)
            {
                Model.Rabbit RabbitOb = _rabbitService.GetRabbit(RabbitRegNo, BreederRegNo);
                Trimming.RabbitRegNo = RabbitOb.RabbitRegNo;
                Trimming.BreederRegNo = RabbitOb.BreederRegNo;
                Trimming.Name = RabbitOb.Name;
            }

            return Page();
        }

        //public async Task<IActionResult> OnPostAsync()
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Page();
        //    }
        //    await _trimmingService.AddTrimmingAsync(Trimming);
        //    return RedirectToPage("GetAllTrimming");
        //}

        /// <summary>
        /// OnPost for create Trimming, creating a new trimming.
        /// </summary>
        /// <param name="trimming"></param>
        /// <returns></returns>
        public async Task<IActionResult> OnPostAsync(Model.Trimming trimming)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await _trimmingService.AddTrimmingAsync(trimming);
            return RedirectToPage($"GetAllTrimming", new { Owner = OwnerId });
        }
    }
}
