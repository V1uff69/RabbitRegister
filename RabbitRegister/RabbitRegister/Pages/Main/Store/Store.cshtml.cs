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
        public void OnGet()
        {
            Yarns = _productService.GetYarns();
            Wools = _productService.GetWools();
        }
        public async Task<IActionResult> OnPostAsync(int id, string type)
        {
            await _storeService.AddToBasketAsync(id, type);

            TempData["Notification"] = "Product added to the basket.";

            return RedirectToPage("/Main/Store/Store");
        }
    }
}
