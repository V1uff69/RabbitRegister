using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RabbitRegister.Model;
using RabbitRegister.Services.ProductService;

namespace RabbitRegister.Pages.Main.Product
{
    [Authorize(Policy = "BreederOnly")] // kun brugere med "BreederOnly" har tilladelse til at få adgang til denne side
    public class DeleteYarnModel : PageModel
    {
        private IProductService _yarnService;

        public DeleteYarnModel(IProductService productService)
        {
            _yarnService = productService;
        }

        [BindProperty]
        public Model.Yarn Yarn { get; set; } // Property til at binde Yarn-objektet til formularen på siden

        public IActionResult OnGet(int Id)
        {
            Yarn = _yarnService.GetYarn(Id); // Henter Yarn-objektet med den angivne Id fra ProductService

            return Page();
        }

        public IActionResult OnPost(int Id)
        {
            _yarnService.DeleteYarnAsync(Id); // Sletter Yarn-objektet med den angivne Id ved hjælp af ProductService

            // Omdirigerer til en anden side og sender breederRegNo som en ruteværdi
            return RedirectToPage("GetAllYarn", new { breederRegNo = User.Identity.Name });
        }
    }
}
