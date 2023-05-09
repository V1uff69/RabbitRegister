using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RabbitRegister.Model;
using RabbitRegister.Services.ProductService;

namespace RabbitRegister.Pages.Main.Product
{
    public class CreateWoolModel : PageModel
    {
        private IProductService _productService;

        public CreateWoolModel(IProductService productService)
        {
            _productService = productService;
        }

        [BindProperty]
        public Model.Wool Wool { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPostAsync(Wool wool)
        {
            _productService.AddWoolAsync(wool);
                
            return RedirectToPage("GetAllWool");
        }
    }
}
