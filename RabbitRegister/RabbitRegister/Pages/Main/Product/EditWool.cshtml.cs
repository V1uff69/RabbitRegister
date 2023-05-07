using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RabbitRegister.Services.ProductService;

namespace RabbitRegister.Pages.Main.Product
{
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
            if (Wool == null) 
                return RedirectToPage("/NotFound");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _productService.UpdateWoolAsync(Wool);
            return RedirectToPage("GetAllWool");
        }
    }
}
