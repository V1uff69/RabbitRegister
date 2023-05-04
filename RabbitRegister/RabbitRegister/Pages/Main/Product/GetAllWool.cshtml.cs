using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RabbitRegister.Model;
using RabbitRegister.Services.ProductService;
using System.Runtime.CompilerServices;

namespace RabbitRegister.Pages.Main.Product
{
    public class GetAllWoolModel : PageModel
    {
        public IEnumerable<Wool> Wools { get; set; }
        public Wool Wool { get; set; } = new Wool();

        private ProductService _productService;

        public GetAllWoolModel(ProductService productService)
        {
            _productService = productService;
        }

        public void OnGet()
        {
          Wools = _productService.GetWools();    
        }

    }
}
