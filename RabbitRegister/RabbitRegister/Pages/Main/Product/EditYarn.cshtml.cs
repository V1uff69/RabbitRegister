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

		public IActionResult OnGet(int Id)
		{
			Yarn = _yarnService.GetYarn(Id);
			return Page();
		}

		public IActionResult OnPost()
		{
			_yarnService.UpdateYarnAsync(Yarn);
			return RedirectToPage("GetAllYarn");
		}
	}
}
