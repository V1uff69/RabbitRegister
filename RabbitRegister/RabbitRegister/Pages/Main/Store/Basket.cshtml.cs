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

        public async Task<IActionResult> OnPostDecreaseAmount(int id)
        {
            // Retrieve the orderLine using the id parameter
            OrderLine orderLine = _storeService.GetOrderLine(id);

            if (orderLine != null)
            {
               await _storeService.DecreaseAmount(orderLine, id);
            }
            return Page();
        }

        public async Task<IActionResult> OnPostIncreaseAmount(int id)
        {
            // Retrieve the orderLine using the id parameter
            OrderLine orderLine = _storeService.GetOrderLine(id);

            if (orderLine != null)
            {
                await _storeService.IncreaseAmount(orderLine, id);
            }
            return Page();
        }

        public IActionResult OnPostAsync()
        {
            return Page();
        }
    }
}
