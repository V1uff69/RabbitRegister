using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using RabbitRegister.Services.BreederService;

namespace RabbitRegister.Pages.Main.LogIn
{
    public class LogInPageModel : PageModel
    {

        private IBreederService _breederService { get; set; } //Interfacet for BreederService

        [BindProperty]
        public int BreederRegNo { get; set; }

        [BindProperty, DataType(DataType.Password)]
        public string Password { get; set; }
        public string Message { get; set; }

        public LogInPageModel(IBreederService breederService)
        {
            _breederService = breederService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            List<Model.Breeder> breeders = _breederService.Breeders;

            // Går igennem hver avler der er oprettet for at finde en, der matcher det indtastede avlerId
            foreach (Model.Breeder breeder in breeders)
            {
                if (BreederRegNo == breeder.BreederRegNo)
                {
                    var passwordHasher = new PasswordHasher<string>();

                    // Verificer adgangskoden ved at sammenligne den med den gemte hashet
                    if (passwordHasher.VerifyHashedPassword(null, breeder.Password, Password) == PasswordVerificationResult.Success)
                    {
                        string UserName = BreederRegNo.ToString();

                        // Opretter en liste af påstande (claims) for brugeren, herunder navn og rolle
                        var claims = new List<Claim> { new Claim(ClaimTypes.Name, UserName), new Claim(ClaimTypes.Role, "Breeder") };

                        if (breeder.isAdmin)
                        {
                            // Tilføjer rollen "Admin" til påstandene, hvis avleren er en administrator
                            claims.Add(new Claim(ClaimTypes.Role, "Admin"));
                        }

                        // Opretter en identitet (claims identity) baseret på de angivne påstande
                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                        // Konfigurer autentifikationsegenskaberne for cookies
                        var authProperties = new AuthenticationProperties
                        {
                            IsPersistent = true, // Husk brugerens autentifikation på tværs af anmodninger
                            ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1) // Indstil udløbstiden for autentifikationscookie'en
                        };

                        // Logger brugeren ind ved at oprette en autentifikations-cookie med den angivne identitet og egenskaber
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                        // Omdiriger til "/Index" siden efter succesfuld login
                        return RedirectToPage("/Index");
                    }
                }
            }

            // Hvis ingen avler matcher det indtastede avler Id eller adgangskoden er forkert, vises "Message" med en fejlmeddelelse
            Message = "Forkert Avler-ID eller adgangskode";
            return Page();
        }
    }
}

