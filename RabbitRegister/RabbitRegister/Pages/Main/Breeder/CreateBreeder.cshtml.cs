using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RabbitRegister.Model;
using RabbitRegister.Services.BreederService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNet.Identity;

namespace RabbitRegister.Pages.Main.Breeder
{
        public class CreateBreederModel : PageModel
        {
            private readonly IBreederService _breederService; // Reference til IBreederService, der bruges til at håndtere avlerrelaterede metoder osv
            private readonly PasswordHasher<string> _passwordHasher; // En PasswordHasher-instans, der bruges til at hashe adgangskoden

            [BindProperty]
            public Model.Breeder Breeder { get; set; } = new Model.Breeder(); // Et BindProperty til avlermodellen, der skal udfyldes i formularen på siden

            public CreateBreederModel(IBreederService breederService, PasswordHasher<string> passwordHasher)
            {
                _breederService = breederService; // Gemmer en reference til IBreederService, der er injiceret
                _passwordHasher = passwordHasher; // Opretter en ny instans af PasswordHasher
            }

            public IActionResult OnGet()
            {
                return Page();
            }

            public async Task<IActionResult> OnPostAsync()
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }

                Breeder.Password = _passwordHasher.HashPassword(null, Breeder.Password); // Hasher adgangskoden, før den gemmes i databasen
                await _breederService.AddUserAsync(Breeder); // Kalder metoden AddUserAsync på IBreederService for at tilføje den nye avler til databasen


                return RedirectToPage("GetAllBreeders"); // Omdirigerer til siden, der viser alle avlere
            }
        }
}

