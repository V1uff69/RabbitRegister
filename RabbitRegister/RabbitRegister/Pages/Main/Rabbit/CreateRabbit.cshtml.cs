using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RabbitRegister.Services.RabbitService;

namespace RabbitRegister.Pages.Main.Rabbit
{
    [Authorize(Policy = "BreederOnly")]
    public class CreateRabbitModel : PageModel
    {
        private IRabbitService _rabbitService;


        public CreateRabbitModel(IRabbitService rabbitService)
        {
            _rabbitService = rabbitService;
        }

        [BindProperty]
        public Model.Rabbit Rabbit { get; set; } = new Model.Rabbit();

        public IActionResult OnGet()
        {
            return Page();
        }

        public bool exceptionFound { get; set; }
        public string exceptionText { get; set; }
        /// <summary>
        /// Forsøger at oprette en kanin, hvis ikke ID allerede findes
        /// </summary>
        /// <param name="Rabbit">Kanin objekt som skal tilføjes</param>
        /// <returns>Forsøger at tilføje en kanin og returnere avleren til GetAllRabbits med sit Avler-ID</returns>
        public async Task<IActionResult> OnPostAsync(Model.Rabbit Rabbit)
		{
			if (!ModelState.IsValid)    //Denne del kigger på brugerens INPUT. Er de korrekt udført? 
			{
				return Page();
			}
			try
			{
				await _rabbitService.AddRabbitAsync(Rabbit);
				return RedirectToPage("GetAllRabbits", new { breederRegNo = User.Identity.Name });
			}
			catch (DbUpdateException)
			{
				this.exceptionFound = true;     
				this.exceptionText = "ID Findes allerede!!!";
			}
			return null;
		}

	}
}
