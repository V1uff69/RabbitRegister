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

        private IBreederService _breederService { get; set; }

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
            foreach (Model.Breeder breeder in breeders)
            {
                if (BreederRegNo == breeder.BreederRegNo)
                {
                    var passwordHasher = new PasswordHasher<string>();

                    if (passwordHasher.VerifyHashedPassword(null, breeder.Password, Password) == PasswordVerificationResult.Success)
                    {
                        string UserName = BreederRegNo.ToString();
                        var claims = new List<Claim> { new Claim(ClaimTypes.Name, UserName), new Claim(ClaimTypes.Role, "Breeder") };

                        if (breeder.isAdmin)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, "Admin"));
                        }

                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var authProperties = new AuthenticationProperties
                        {
                            IsPersistent = true, // Remember user authentication across requests
                            ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1) // Set expiration time for the authentication cookie
                        };

                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
                        return RedirectToPage("/Index");
                    }
                }
            }

            Message = "Forkert Avler Id eller Adgangskode";
            return Page();
        }




    }

}

