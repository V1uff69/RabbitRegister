using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RabbitRegister.Model;
using RabbitRegister.Services.ProductService;

namespace RabbitRegister.Pages.Main.Product
{
    public class ProductInfoModel : PageModel
    {
        private IProductService _productService { get; set; }
        public List<Model.Wool> Wools { get; set; }
        public List<Model.Yarn> Yarns { get; set; }
        public Wool wool { get; set; }
        public Yarn yarn { get; set; }

        public ProductInfoModel(IProductService productService)
        {
            _productService = productService;
        }
        public IActionResult OnGet(int Id)
        {
           wool = _productService.GetWools(Id);
            yarn = _productService.GetYarn(Id);
            //if (yarn.ProductType == "Yarn")
            //{
            //    yarn = _productService.GetYarn(Id);
            //}
            //else { 
            //    if (wool.ProductType == "Wool")
            //    wool = _productService.GetWools(Id);
            //}
            return Page();
        }
    }
}
