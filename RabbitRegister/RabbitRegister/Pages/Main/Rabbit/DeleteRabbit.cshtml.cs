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


        public IActionResult OnGet(int rabbitRegNo, int originRegNo)
        {
            Rabbit = _rabbitService.GetRabbit(rabbitRegNo, originRegNo);
            if (Rabbit == null)
            {
                return RedirectToPage("/NotFound");
            }

            return Page();
        }

        /// <summary>
        /// Sletter en kanin med den angivne Id - bemærk der mangler exceptions for hvilken Avler som kan slette den
        /// </summary>
        /// <param name="rabbitRegNo">Første nøgle-del for kaninens composite key</param>
        /// <param name="originRegNo">Anden nøgle-del for kaninens composite key</param>
        /// <returns>Omdirigerer til GetAllRabbits med avlerens, Avler-ID</returns>
        public async Task<IActionResult> OnPostAsync(int rabbitRegNo, int originRegNo)
        {
            Model.Rabbit deletedRabbit = await _rabbitService.DeleteRabbitAsync(rabbitRegNo, originRegNo);
			//if (deletedRabbit == null)
			//    return RedirectToPage("/NotFound"); //NotFound er ikke defineret endnu

			return RedirectToPage("GetAllRabbits");
		}
	}
}
