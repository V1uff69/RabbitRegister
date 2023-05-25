using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RabbitRegister.Model;
using RabbitRegister.Services.ProductService;

namespace RabbitRegister.Pages.Main.Product
{
    [Authorize(Policy = "BreederOnly")]
    public class EditWoolModel : PageModel
    {
        [BindProperty]
        public Model.Wool Wool { get; set; }
        private IProductService _productService;

        public EditWoolModel(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// This method handles the HTTP GET request for retrieving a wool product to be edited.
        /// </summary>
        /// <param name="id">The ID of the wool product.</param>
        /// <returns>The page.</returns>
        public IActionResult OnGet(int id)
        {
            // Retrieve the wool product with the specified ID using ProductService
            Wool = _productService.GetWools(id);

            // Return the current page
            return Page();
        }

        /// <summary>
        /// This method handles the HTTP POST request for updating a wool product.
        /// </summary>
        /// <param name="wool">The updated Wool object from the form.</param>
        public async Task<IActionResult> OnPostAsync(Wool wool)
        {
            // Check if the model is valid (based on validation attributes in the Wool class)
            if (!ModelState.IsValid)
            {
                // If the model is invalid, return the current page
                return Page();
            }

            // Update the wool product asynchronously using ProductService
            await _productService.UpdateWoolAsync(wool);

            // Redirect to the "GetAllWool" page with the breederRegNo parameter set to the current user's name
            return RedirectToPage("GetAllWool", new { breederRegNo = User.Identity.Name });
        }
    }

}
