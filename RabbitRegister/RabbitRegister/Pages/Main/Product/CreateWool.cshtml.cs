using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RabbitRegister.Model;
using RabbitRegister.Services.ProductService;

namespace RabbitRegister.Pages.Main.Product
{
    [Authorize(Policy = "BreederOnly")]
    public class CreateWoolModel : PageModel
    {
        private IProductService _productService { get; set; }
        public Wool Wool { get; set; } = new Wool();

        public CreateWoolModel(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// This method handles the HTTP GET request for displaying the create wool page.
        /// </summary>
        /// <returns>The page.</returns>
        public IActionResult OnGet()
        {
            // Return the current page
            return Page();
        }

        /// <summary>
        /// This method handles the HTTP POST request for creating a new wool product.
        /// </summary>
        /// <param name="wool">The Wool object containing the data from the form.</param>
        public async Task<IActionResult> OnPostAsync(Wool wool)
        {
            // Check if the model is valid (based on validation attributes in the Wool class)
            if (!ModelState.IsValid)
            {
                // If the model is invalid, return the current page
                return Page();
            }

            // Add the wool product asynchronously using ProductService
            await _productService.AddWoolAsync(wool);

            // Redirect to the "GetAllWool" page with the breederRegNo parameter set to the current user's name
            return RedirectToPage("GetAllWool", new { breederRegNo = User.Identity.Name });
        }
    }

}
