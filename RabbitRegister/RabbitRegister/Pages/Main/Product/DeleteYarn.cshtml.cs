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

		public static int Id { get; set; }
		public int YarnId = Id;

		public IActionResult OnGet(int YarnId)
		{
			Yarn = _yarnService.GetYarn(YarnId);
			

			return Page();
		}

		public IActionResult OnPost(int Id)
		{
			_yarnService.DeleteYarnAsync(Id);
			return RedirectToPage("GetAllYarn");
		}
	}
}
