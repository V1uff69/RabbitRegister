using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RabbitRegister.EFDbContext;
using RabbitRegister.Model;
using RabbitRegister.Services.Store;

namespace RabbitRegister.Pages.Main.Store
{
    public class CreateOrderModel : PageModel
    {
        [BindProperty]
        public Model.Order Order { get; set; } = new Model.Order();
        public List<OrderLine> _orderLines { get; set; }
        public List<Model.Wool> Wools { get; set; }
        public List<Model.Yarn> Yarns { get; set; }
        public Model.Product Product { get; set; }
        private IStoreService _storeService { get; set; }
        public ItemDbContext dbContext { get; set; }

        public CreateOrderModel(IStoreService storeService, ItemDbContext DbContext)
        {
            _storeService = storeService;
            dbContext = DbContext;
        }
        public void OnGet()
        {
            _orderLines = _storeService.GetBasket();
        }

        public async Task<IActionResult> OnPost(Order order)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await _storeService.AddOrderAsync(order);
            return RedirectToPage("/Main/Store/OrderConfirmation");
        }


    }
}
