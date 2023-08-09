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

        /// <summary>
        /// Edit en kanins properties ud fra dens Id - bemærk der mangler exceptions for hvilken Avler som kan edit den
        /// </summary>
        /// <param name="id">Første nøgle-del for kaninens composite key(RabbitRegNo)</param>
        /// <param name="breederRegNo">Anden nøgle-del for kaninens composite key</param>
        /// <returns>Omdirigerer til GetAllRabbits med avlerens, Avler-ID</returns>
        public async Task<IActionResult> OnPostAsync(int id, int breederRegNo)
		{
			//if (ModelState.IsValid)
			//{
			//	return Page();
			//}

			await _rabbitService.UpdateRabbitAsync(Rabbit, id, breederRegNo);
			return RedirectToPage("GetAllRabbits", new { breederRegNo = User.Identity.Name});
		}
	}
}
