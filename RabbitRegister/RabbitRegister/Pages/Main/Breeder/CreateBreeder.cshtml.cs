using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RabbitRegister.Model;
using RabbitRegister.Services.BreederService;
using RabbitRegister.Pages.Main.Breeder;

namespace RabbitRegister.Pages.Main.Breeder
{
    public class CreateBreederModel : PageModel
    {
        private IBreederService _breederService;


        public CreateBreederModel(IBreederService breederService)
        {
            _breederService = breederService;
        }

        [BindProperty]
        public Model.Breeder Breeder { get; set; } = new Model.Breeder();

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Model.Breeder breeder)
        {
            await _breederService.AddUserAsync(breeder);
            return RedirectToPage("GetAllBreeders");
        }
    }
}
