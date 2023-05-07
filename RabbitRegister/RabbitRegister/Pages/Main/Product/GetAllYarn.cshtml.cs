using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RabbitRegister.Model;
using RabbitRegister.Services.ProductService;

namespace RabbitRegister.Pages.Main.Product
{
    public class GetAllYarnModel : PageModel
    {
		private IProductService _yarnService;

		public GetAllYarnModel(IProductService productService)
		{
			_yarnService = productService;
		}

		public List<Model.Yarn>? Yarns { get; private set; }


		public void OnGet()
		{
			Yarns = _yarnService.GetYarns();
		}
	}
}
