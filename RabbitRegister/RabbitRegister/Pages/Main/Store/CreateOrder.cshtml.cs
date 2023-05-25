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
            // Get the order lines from the store service and assign them to the _orderLines property
            _orderLines = _storeService.GetBasket();
        }

        // This method is called when the form is submitted via HTTP POST
        public async Task<IActionResult> OnPost(Order order)
        {
            // Check if the submitted order model is valid
            if (!ModelState.IsValid)
            {
                // If the model is not valid, return the current page with the validation errors
                return Page();
            }

            // Add the order asynchronously using the store service
            await _storeService.AddOrderAsync(order);

            // Redirect the user to the OrderConfirmation page
            return RedirectToPage("/Main/Store/OrderConfirmation");
        }



    }
}
