using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RabbitRegister.Model;
using RabbitRegister.Services.ProductService;
using System.Runtime.CompilerServices;

namespace RabbitRegister.Pages.Main.Product
{
    public class GetAllWoolModel : PageModel
    {
        private IProductService _productService { get; set; }
        public List<Model.Wool> Wools { get; set; }

        public GetAllWoolModel(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// This method handles the HTTP GET request for retrieving wool creations of a specific breeder.
        /// </summary>
        /// <param name="breederRegNo">The registration number of the breeder.</param>
        /// <returns>The IActionResult representing the page.</returns>
        public IActionResult OnGet(int breederRegNo)
        {
            // Retrieve wool creations of the specified breeder using ProductService
            Wools = _productService.GetMyWoolCreations(breederRegNo);

            // Return the current page
            return Page();
        }

    }
}
