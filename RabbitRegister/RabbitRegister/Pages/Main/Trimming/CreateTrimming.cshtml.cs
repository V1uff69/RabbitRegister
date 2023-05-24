using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RabbitRegister.Services.RabbitService;
using RabbitRegister.Services.TrimmingService;

namespace RabbitRegister.Pages.Main.Trimming
{
    [Authorize(Policy = "BreederOnly")]
    public class CreateTrimmingModel : PageModel
    {

        private ITrimmingService _trimmingService;
        private IRabbitService _rabbitService;

        public CreateTrimmingModel(ITrimmingService trimmingService, IRabbitService rabbitService)
        {
            _trimmingService = trimmingService;
            _rabbitService = rabbitService;
        }

        [BindProperty]
        public Model.Trimming Trimming { get; set; } = new Model.Trimming();

        [FromQuery(Name = "Owner")]
        public int OwnerId { get; set; }

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

        public async Task<IActionResult> OnPostAsync(Model.Trimming trimming)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await _trimmingService.AddTrimmingAsync(trimming);
            return RedirectToPage($"GetAllTrimming",new { Owner = OwnerId});
        }
    }
}
