using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RabbitRegister.Services.BreederService;
using RabbitRegister.Services.ProductService;

namespace RabbitRegister.Pages.Main.Breeder
{
    public class DeleteBreederModel : PageModel
    {
        private IBreederService _breederService;

        public DeleteBreederModel(IBreederService breederService)
        {
            _breederService = breederService;
        }

        [BindProperty]
        public Model.Breeder Breeder { get; set; }

        public IActionResult OnGet(int id)
        {
            Breeder = _breederService.GetBreeder(id);

            return Page();
        }

        public IActionResult OnPost(int id)
        {
            _breederService.DeleteBreederAsync(id);
            return RedirectToPage("GetAllBreeders");
        }
    }
}
