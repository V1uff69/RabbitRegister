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
        private IBreederService _breederService;
        private PasswordHasher<string> _passwordHasher;


        public EditBreederModel(IBreederService breederService)
        {
            _breederService = breederService;
            _passwordHasher = new PasswordHasher<string>();

        }

        public Model.Breeder Breeder { get; set; }


        public IActionResult OnGet(int Id)
        {
            Breeder = _breederService.GetBreeder(Id);
            return Page();
        }

        public async Task<IActionResult> OnPost(Model.Breeder breeder)
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }
            breeder.Password = _passwordHasher.HashPassword(null, breeder.Password);
            await _breederService.UpdateBreederAsync(breeder);
            if (User.IsInRole("Admin"))
            {
                return RedirectToPage("GetAllBreeders");
            }
            else
            {
                return RedirectToPage("/Index");
            }
            //return RedirectToPage("GetAllBreeders");
        }
    }
}
