using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RabbitRegister.Model;
using RabbitRegister.Services.ProductService;
using RabbitRegister.Services.Store;


namespace RabbitRegister.Pages.Main.Store
{
    public class BasketModel : PageModel
    {
        private IStoreService _storeService { get; set; }
        private IProductService _productService { get; set; }
        [BindProperty] public Order _order { get; set; } = new Order();
        public List<OrderLine> OrderLines { get; set; } = new List<OrderLine>();
        public List<Model.Wool> Wools { get; set; }
        public List<Model.Yarn> Yarns { get; set; }

        public BasketModel(IStoreService storeService, IProductService productService)
        {
            _storeService = storeService;
            _productService = productService;
        }

        public void OnGet()
        {
            OrderLines = _storeService.GetBasket();

        }

        public async Task<IActionResult> OnGetAddToBasket(int id)
        {

            OrderLines = _storeService.GetBasket();
            OrderLine item = OrderLines.Where(p => p.ProductId == id).FirstOrDefault();


            return Page();

        }

        public async Task<IActionResult> OnGetDecreaseAmount(int id)
        {
            OrderLine thisOrderLine = _storeService.GetOrderLine(id);

            if (thisOrderLine != null)
            {
                await _storeService.DecreaseAmount(thisOrderLine, id);
            }

            return RedirectToPage("/Main/Store/Basket"); 
        }

        public async Task<IActionResult> OnGetIncreaseAmount(int id)
        {
            OrderLine thisOrderLine = _storeService.GetOrderLine(id);

            if (thisOrderLine != null)
            {
                await _storeService.IncreaseAmount(thisOrderLine, id);
            }

            return RedirectToPage("/Main/Store/Basket");
        } 

        public IActionResult OnPostAsync()
        {
            return Page();
        }
    }
}
