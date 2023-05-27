using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RabbitRegister.Model;
using RabbitRegister.Services.BreederService;
using RabbitRegister.Pages.Main.Breeder;
using RabbitRegister.Services.ProductService;
using Microsoft.AspNetCore.Identity;

namespace RabbitRegister.Pages.Main.Breeder
{
    public class EditBreederModel : PageModel
    {
        private IBreederService _breederService; // Reference til IBreederService, der bruges til at håndtere avlerrelaterede metoder
        private PasswordHasher<string> _passwordHasher; // Reference til PasswordHasher, der bruges til at hashe adgangskoden

        public EditBreederModel(IBreederService breederService)
        {
            _breederService = breederService; // Gemmer en reference til IBreederService, der er injiceret
            _passwordHasher = new PasswordHasher<string>(); // Opretter en ny instans af PasswordHasher
        }

        public Model.Breeder Breeder { get; set; } // property til at gemme avleren, der skal redigeres

        public IActionResult OnGet(int Id)
        {
            Breeder = _breederService.GetBreeder(Id); // Henter avleren med den angivne id fra databasen og gemmer den i Breeder-egenskaben
            return Page();
        }

        public async Task<IActionResult> OnPost(Model.Breeder breeder)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            breeder.Password = _passwordHasher.HashPassword(null, breeder.Password); // Hasher adgangskoden, før den gemmes i databasen
            await _breederService.UpdateBreederAsync(breeder); // Opdaterer avleren i databasen ved at kalde UpdateBreederAsync-metoden på IBreederService

            if (User.IsInRole("Admin")) // Tjekker om brugeren har rollen "Admin"
            {
                return RedirectToPage("GetAllBreeders"); // Omdirigerer til siden, der viser alle avlere
            }
            else
            {
                return RedirectToPage("/Index"); // Omdirigerer til startsiden ("/Index")
            }
        }
    }
}
