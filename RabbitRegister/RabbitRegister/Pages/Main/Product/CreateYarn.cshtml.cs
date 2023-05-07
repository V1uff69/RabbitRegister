using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RabbitRegister.Model;
using RabbitRegister.Services.ProductService;

namespace RabbitRegister.Pages.Main.Product
{
    public class CreateYarnModel : PageModel
    {
		private IProductService _yarnService;

		public CreateYarnModel(IProductService productService)
		{
			_yarnService = productService;
		}

		[BindProperty]
		public Model.Yarn Yarn { get; set; }

		public IActionResult OnGet()
		{
			return Page();
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}
			await _yarnService.AddYarnAsync(Yarn);
			return RedirectToPage("GetAllYarn");
		}
	}
}
