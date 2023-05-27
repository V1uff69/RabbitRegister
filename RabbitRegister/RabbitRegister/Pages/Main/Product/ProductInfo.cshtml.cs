using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RabbitRegister.Model;
using RabbitRegister.Services.ProductService;
using RabbitRegister.Services.Store;

namespace RabbitRegister.Pages.Main.Product
{
    public class ProductInfoModel : PageModel
    {
        private IProductService _productService { get; set; }
        private IStoreService _storeService { get; set; }
        public List<Model.Wool> Wools { get; set; }
        public List<Model.Yarn> Yarns { get; set; }
        public Wool Wool { get; set; }
        public Yarn Yarn { get; set; }

        public ProductInfoModel(IProductService productService, IStoreService storeService)
        {
            _productService = productService;
            _storeService = storeService;
            _storeService = storeService;
        }

        /// <summary>
        /// This method handles the HTTP GET request for retrieving product details.
        /// </summary>
        /// <param name="Id">The ID of the product.</param>
        /// <param name="type">The type of the product ("Wool" or "Yarn").</param>
        /// <returns>The page.</returns>
        public IActionResult OnGet(int Id, string type)
        {
            if (type == "Wool")
            {
                // Retrieve Wool product details using ProductService
                Wool = _productService.GetWools(Id);
            }
            else if (type == "Yarn")
            {
                // Retrieve Yarn product details using ProductService
                Yarn = _productService.GetYarn(Id);
            }

            // Return the current page
            return Page();
        }

        /// <summary>
        /// This method handles the HTTP POST request for adding a product to the basket.
        /// </summary>
        /// <param name="id">The ID of the product.</param>
        /// <param name="type">The type of the product ("Wool" or "Yarn").</param>
        public async Task<IActionResult> OnPostAsync(int id, string type)
        {
            // Add the product to the basket asynchronously using StoreService
            await _storeService.AddToBasketAsync(id, type);

            // Set a notification message to be displayed on the next page
            TempData["Notification"] = "Product added to the basket.";

            // Redirect to the "/Main/Store/Store" page
            return RedirectToPage("/Main/Store/Store");
        }

    }
}
