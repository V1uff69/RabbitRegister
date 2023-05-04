using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RabbitRegister.Services.ProductService;

namespace RabbitRegister.Pages.Main.Product
{
	public class EditYarnModel : PageModel
	{
		private IProductService _yarnService;

		public EditYarnModel(IProductService productService)
		{
			_yarnService = productService;
		}

		[BindProperty]
		public Model.Yarn Yarn { get; set; }

		public IActionResult OnGet(int yarnId)
		{
			Yarn = _yarnService.GetYarn(yarnId);
			if (Yarn == null)
				return RedirectToPage("/NotFound");

			return Page();
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			await _yarnService.UpdateYarnAsync(Yarn);
			return RedirectToPage("GetAllYarn");
		}
	}
}
