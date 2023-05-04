using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RabbitRegister.Model;
using RabbitRegister.Services.ProductService;

namespace RabbitRegister.Pages.Main.Product
{
    public class GetAllYarnModel : PageModel
    {
        public IEnumerable<Yarn> Yarns { get; set; }
        public Yarn Yarn { get; set; } = new Yarn();

        private ProductService _productService;

        public GetAllYarnModel(ProductService productService)
        {
            _productService = productService;
        }

        public void OnGet()
        {
            Yarns = _productService.GetYarns();
        }
    }
}
