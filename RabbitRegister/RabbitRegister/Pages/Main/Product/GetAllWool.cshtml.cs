using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RabbitRegister.Model;
using RabbitRegister.Services.ProductService;
using System.Runtime.CompilerServices;

namespace RabbitRegister.Pages.Main.Product
{
    public class GetAllWoolModel : PageModel
    {
        private IProductService _productService { get; set; }
        public List<Model.Wool> Wools { get; set; }

        public GetAllWoolModel(IProductService productService)
        {
            _productService = productService;
        }

		public IActionResult OnGet(int breederRegNo)
		{
			Wools = _productService.GetMyWoolCreations(breederRegNo);

			return Page();
		}

		//public void OnGet()
		//{
		//    Wools = _productService.GetWools();
		//}

		public void OnPost()
        {

        }
    }
}
