using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RabbitRegister.Model;
using RabbitRegister.Services.BreederService;
using RabbitRegister.Pages.Main.Breeder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNet.Identity;

namespace RabbitRegister.Pages.Main.Breeder
{
    public class CreateBreederModel : PageModel
    {
        private IBreederService _breederService; // Reference til IBreederService, der bruges til at håndtere avlerrelaterede metoder osv
        private PasswordHasher<string> _passwordHasher; // En PasswordHasher-instans, der bruges til at hashe adgangskoden

        public CreateBreederModel(IBreederService breederService)
        {
            _breederService = breederService; // Gemmer en reference til IBreederService, der er injiceret

            _passwordHasher = new PasswordHasher<string>(); // Opretter en ny instans af PasswordHasher
        }

        [BindProperty]
        public Model.Breeder Breeder { get; set; } = new Model.Breeder(); // Et BindProperty til avlermodellen, der skal udfyldes i formularen på siden

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Model.Breeder breeder)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            breeder.Password = _passwordHasher.HashPassword(null, breeder.Password); // Hasher adgangskoden, før den gemmes i databasen

            await _breederService.AddUserAsync(breeder); // Kalder metoden AddUserAsync på IBreederService for at tilføje den nye avler til databasen

            return RedirectToPage("GetAllBreeders"); // Omdirigerer til siden, der viser alle avlere
        }
    }
}
