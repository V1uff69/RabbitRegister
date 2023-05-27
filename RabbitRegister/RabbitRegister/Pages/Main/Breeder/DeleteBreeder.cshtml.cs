using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RabbitRegister.Services.BreederService;
using RabbitRegister.Services.ProductService;

namespace RabbitRegister.Pages.Main.Breeder
{
    public class DeleteBreederModel : PageModel
    {
        private IBreederService _breederService; // Reference til IBreederService, der bruges til at håndtere avlerrelaterede metoder osv

        public DeleteBreederModel(IBreederService breederService)
        {
            _breederService = breederService; // Gemmer en reference til IBreederService, der er injiceret
        }

        [BindProperty]
        public Model.Breeder Breeder { get; set; } // Et BindProperty til avlermodellen, der skal slettes

        public IActionResult OnGet(int id)
        {
            Breeder = _breederService.GetBreeder(id); // Henter avleren med den angivne id fra databasen og gemmer den i Breeder-egenskaben

            return Page();
        }

        public IActionResult OnPost(int id)
        {
            _breederService.DeleteBreederAsync(id); // Sletter avleren med den angivne id fra databasen ved at kalde DeleteBreederAsync-metoden på IBreederService

            return RedirectToPage("GetAllBreeders"); // Omdirigerer til siden, der viser alle avlere
        }
    }
}
