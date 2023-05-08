using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RabbitRegister.Model;
using RabbitRegister.Services.ProductService;

namespace RabbitRegister.Pages.Main.Product
{
    public class DeleteWoolModel : PageModel
    {
        private IProductService _productService;

        public DeleteWoolModel(IProductService productService)
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

        public IActionResult OnPost(int WoolId)
        {
            _productService.DeleteWoolAsync(WoolId);
            return RedirectToPage("GetAllWool");
        }
    }
}
