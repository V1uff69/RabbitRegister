using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RabbitRegister.Model;
using RabbitRegister.Services.ProductService;

namespace RabbitRegister.Pages.Main.Product
{
    [Authorize(Policy = "BreederOnly")]
    public class DeleteYarnModel : PageModel
    {

		private IProductService _yarnService;

		public DeleteYarnModel(IProductService productService)
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

		public IActionResult OnPost(int Id)
		{
			_yarnService.DeleteYarnAsync(Id);
			return RedirectToPage("GetAllYarn", new { breederRegNo = User.Identity.Name });
		}
	}
}
