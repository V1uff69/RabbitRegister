using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RabbitRegister.Services.ProductService;

namespace RabbitRegister.Pages.Main.Store
{
    public class StoreModel : PageModel
    {
        private IProductService _productService { get; set; }
        public List<Model.Wool> Wools { get; set; }
        public List<Model.Yarn> Yarns { get; set; }

        public StoreModel(IProductService productService)
        {
            _productService = productService;
        }
        public void OnGet()
        {
            Yarns = _productService.GetYarns();
            Wools = _productService.GetWools();
        }
    }
}
