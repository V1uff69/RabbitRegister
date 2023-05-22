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
        private IBreederService _breederService;
        private PasswordHasher<string> _passwordHasher;


        public CreateBreederModel(IBreederService breederService)
        {
            _breederService = breederService;

            _passwordHasher = new PasswordHasher<string>();
        }

        [BindProperty]
        public Model.Breeder Breeder { get; set; } = new Model.Breeder();

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
            breeder.Password = _passwordHasher.HashPassword(null, breeder.Password);
            await _breederService.AddUserAsync(breeder);
            return RedirectToPage("GetAllBreeders");
        }
    }
}
