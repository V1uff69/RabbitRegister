using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RabbitRegister.Model;
using RabbitRegister.Services.ProductService;

namespace RabbitRegister.Pages.Main.Product
{
    [Authorize(Policy = "BreederOnly")]
    public class DeleteWoolModel : PageModel
    {
        private IProductService _productService;

        public DeleteWoolModel(IProductService productService)
        {
            _productService = productService;
        }

        [BindProperty]
        public Wool Wool { get; set; }

        /// <summary>
        /// This method handles the HTTP GET request for retrieving a wool product to be deleted.
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
        /// This method handles the HTTP POST request for deleting a wool product.
        /// </summary>
        /// <param name="Id">The ID of the wool product to be deleted.</param>
        public async Task<IActionResult> OnPostAsync(int Id)
        {
            // Delete the wool product asynchronously using ProductService
            await _productService.DeleteWoolAsync(Id);

            // Redirect to the "GetAllWool" page with the breederRegNo parameter set to the current user's name
            return RedirectToPage("GetAllWool", new { breederRegNo = User.Identity.Name });
        }
    }

}
