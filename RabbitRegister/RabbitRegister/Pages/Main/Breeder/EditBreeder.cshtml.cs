using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RabbitRegister.Model;
using RabbitRegister.Services.BreederService;
using RabbitRegister.Pages.Main.Breeder;

namespace RabbitRegister.Pages.Main.Breeder
{
    public class EditBreederModel : PageModel
    {
        private IBreederService _breederService;

        public EditBreederModel(IBreederService breederService)
        {
            _breederService = breederService;
        }

        public Model.Breeder Breeder { get; set; }

        public IActionResult OnGet(int Id)
        {
            Breeder = _breederService.GetBreeder(Id);
            return Page();
        }

        public IActionResult OnPost(int id)
        {
            _breederService.UpdateBreederAsync(Breeder, id);
            return RedirectToPage("GetAllBreeders");
        }
    }
}
