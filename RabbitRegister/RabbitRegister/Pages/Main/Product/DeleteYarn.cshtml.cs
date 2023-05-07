using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RabbitRegister.Services.ProductService;

namespace RabbitRegister.Pages.Main.Product
{
    public class DeleteYarnModel : PageModel
    {
		private IProductService _yarnService;

		public DeleteYarnModel(IProductService productService)
		{
			_yarnService = productService;
		}

		[BindProperty]
		public Model.Yarn Yarn { get; set; }


		public IActionResult OnGet(int YarnId)
		{
			Yarn = _yarnService.GetYarn(YarnId);
			if (Yarn == null)
				return RedirectToPage("/NotFound");

			return Page();
		}

		public async Task<IActionResult> OnPostAsync()
		{
			Model.Yarn deletedYarn = await _yarnService.DeleteYarnAsync(Yarn.YarnId);
			if (deletedYarn == null)
				return RedirectToPage("/NotFound");

			return RedirectToPage("GetAllYarn");
		}
	}
}
