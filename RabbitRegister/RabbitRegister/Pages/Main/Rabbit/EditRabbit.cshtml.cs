using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RabbitRegister.Model;
using RabbitRegister.Services.RabbitService;

namespace RabbitRegister.Pages.Main.Rabbit
{
    [Authorize(Policy = "BreederOnly")]
    public class EditRabbitModel : PageModel
    {
        private IRabbitService _rabbitService;

        public EditRabbitModel(IRabbitService rabbitService)
        {
            _rabbitService = rabbitService;
        }


        [BindProperty]
        public Model.Rabbit Rabbit { get; set; }

        public IActionResult OnGet(int id, int breederRegNo)
        {
            Rabbit = _rabbitService.GetRabbit(id, breederRegNo);
            return Page();
        }

   		public async Task<IActionResult> OnPostAsync(int id, int breederRegNo)
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}
			await _rabbitService.UpdateRabbitAsync(Rabbit, id, breederRegNo);
			return RedirectToPage("GetAllRabbits", new { breederRegNo = User.Identity.Name});
		}
	}
}
