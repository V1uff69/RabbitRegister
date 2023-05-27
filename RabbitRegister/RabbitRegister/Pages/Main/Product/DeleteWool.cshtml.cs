using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RabbitRegister.Model;
using RabbitRegister.Services.ProductService;

namespace RabbitRegister.Pages.Main.Product
{
    [Authorize(Policy = "BreederOnly")]
    public class DeleteWoolModel : PageModel
    {
        private IProductService _productService;

        public DeleteWoolModel(IProductService productService)
        {
            _productService = productService;
        }

        [BindProperty]
        public Wool Wool { get; set; }

        public IActionResult OnGet(int id)
        {
            Wool = _productService.GetWools(id);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int Id)
        {
            await _productService.DeleteWoolAsync(Id);
			return RedirectToPage("GetAllWool", new { breederRegNo = User.Identity.Name });
		}
	}
}
