using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RabbitRegister.Model;
using RabbitRegister.Services.ProductService;
using System.Runtime.CompilerServices;

namespace RabbitRegister.Pages.Main.Product
{
    public class CreateWoolModel : PageModel
    {
        private IProductService _productService { get; set; }
        public List<Model.Wool> Wools { get; set; } 
        public Wool Wool { get; set; } = new Wool();

        public CreateWoolModel(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Wool wool)
        {
            await _productService.AddWoolAsync(wool);
                
            return RedirectToPage("GetAllWool");
            
        }
        
    }
}
