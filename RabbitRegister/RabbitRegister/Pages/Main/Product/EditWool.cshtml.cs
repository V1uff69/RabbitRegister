using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RabbitRegister.Model;
using RabbitRegister.Services.ProductService;

namespace RabbitRegister.Pages.Main.Product
{
    [Authorize(Policy = "BreederOnly")]
    public class EditWoolModel : PageModel
    {
        private IProductService _productService;

        public EditWoolModel(IProductService productService)
        {
            _productService = productService;
        }

        [BindProperty]
        public Model.Wool Wool { get; set; }

        public IActionResult OnGet(int id)
        {
            Wool = _productService.GetWools(id);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Wool Wool)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await _productService.UpdateWoolAsync(Wool);
			return RedirectToPage("GetAllWool", new { breederRegNo = User.Identity.Name });
		}
	}
}
