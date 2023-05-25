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

        public IActionResult OnGet(int Id, string type)
        {
            if (type == "Wool")
            {
                Wool = _productService.GetWools(Id);
            }
            else if (type == "Yarn")
            {
                Yarn = _productService.GetYarn(Id);
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int id, string type)
        {
            await _storeService.AddToBasketAsync(id, type);

            TempData["Notification"] = "Product added to the basket.";

            return RedirectToPage("/Main/Store/Store");
        }
    }
}
