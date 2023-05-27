using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RabbitRegister.Model;
using RabbitRegister.Services.ProductService;

namespace RabbitRegister.Pages.Main.Product
{
    [Authorize(Policy = "BreederOnly")]
    public class CreateWoolModel : PageModel
    {
        private IProductService _productService { get; set; }
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
			if (!ModelState.IsValid)
			{
				return Page();
			}
			await _productService.AddWoolAsync(wool);
                
            return RedirectToPage("GetAllWool", new { breederRegNo = User.Identity.Name });

		}
        
    }
}
