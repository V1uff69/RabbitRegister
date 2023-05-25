using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RabbitRegister.Services.ProductService;
using RabbitRegister.Services.Store;

namespace RabbitRegister.Pages.Main.Store
{
    public class StoreModel : PageModel
    {
        private IProductService _productService { get; set; }
        private IStoreService _storeService { get; set; }

        public List<Model.Wool> Wools { get; set; }
        public List<Model.Yarn> Yarns { get; set; }
        public Model.Product Product { get; set; }
        public Model.Order Order { get; set; }

        public StoreModel(IProductService productService, IStoreService storeService)
        {
            _productService = productService;
            _storeService = storeService;
        }
        // This method is called when the page is requested via HTTP GET
        public void OnGet()
        {
            // Get the list of yarns and wools from the product service
            Yarns = _productService.GetYarns();
            Wools = _productService.GetWools();
        }

        // This method is called when the form is submitted via HTTP POST
        public async Task<IActionResult> OnPostAsync(int id, string type)
        {
            // Add the selected product to the basket asynchronously
            await _storeService.AddToBasketAsync(id, type);

            // Set a temporary data value to display a notification message
            TempData["Notification"] = "Product added to the basket.";

            // Redirect the user back to the Store page
            return RedirectToPage("/Main/Store/Store");
        }
    }
}
