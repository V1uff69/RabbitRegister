using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RabbitRegister.Services.RabbitService;

namespace RabbitRegister.Pages.Main.Rabbit
{
    [Authorize(Policy = "BreederOnly")]
    public class DeleteRabbitModel : PageModel
    {
        private IRabbitService _rabbitService;

        public DeleteRabbitModel(IRabbitService rabbitService)
        {
            _rabbitService = rabbitService;
        }

        [BindProperty]
        public Model.Rabbit Rabbit { get; set; }


        public IActionResult OnGet(int id, int breederRegNo)
        {
            Rabbit = _rabbitService.GetRabbit(id, breederRegNo);
            if (Rabbit == null)
            {
                return RedirectToPage("/NotFound");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id, int breederRegNo)
        {
            Model.Rabbit deletedRabbit = await _rabbitService.DeleteRabbitAsync(id, breederRegNo);
			//if (deletedRabbit == null)
			//    return RedirectToPage("/NotFound"); //NotFound er ikke defineret endnu

			return RedirectToPage("GetAllRabbits", new { breederRegNo = User.Identity.Name });
		}
	}
}
