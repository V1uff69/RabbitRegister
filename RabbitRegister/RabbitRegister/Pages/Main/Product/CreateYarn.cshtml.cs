using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RabbitRegister.Model;
using RabbitRegister.Services.ProductService;

namespace RabbitRegister.Pages.Main.Product
{
    [Authorize(Policy = "BreederOnly")] // kun brugere med "BreederOnly" har tilladelse til at få adgang til denne side
    public class CreateYarnModel : PageModel
    {
        private IProductService _yarnService;

        public CreateYarnModel(IProductService productService)
        {
            _yarnService = productService;
        }

        [BindProperty]
        public Yarn Yarn { get; set; } = new Yarn(); // Property til at binde Yarn-objektet til formularen på siden

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Yarn yarn)
        {
            if (!ModelState.IsValid) // Validerer om modelens tilstand er gyldig
            {
                return Page(); // Returnerer samme side med fejl, hvis modellen er ugyldig
            }

            await _yarnService.AddYarnAsync(yarn); // Kalder en metode i ProductService for at tilføje garnet til databasen

            // Omdirigerer til en anden side og sender breederRegNo som en ruteværdi
            return RedirectToPage("GetAllYarn", new { breederRegNo = User.Identity.Name });
        }
    }
}
