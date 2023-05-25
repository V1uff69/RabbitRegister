using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using RabbitRegister.EFDbContext;
using RabbitRegister.Model;
using RabbitRegister.Services.ProductService;
using RabbitRegister.Services.Store;
using static NuGet.Packaging.PackagingConstants;


namespace RabbitRegister.Pages.Main.Store
{
    public class BasketModel : PageModel
    {
        public List<OrderLine> _orderLines { get; set; }
        public List<Model.Wool> Wools { get; set; }
        public List<Model.Yarn> Yarns { get; set; }
        public Model.Product Product { get; set; }
        private IStoreService _storeService { get; set; }
        public ItemDbContext dbContext { get; set; }

        /// <summary>
        /// Initializes a new instance of the BasketModel class.
        /// </summary>
        /// <param name="storeService">The store service dependency.</param>
        /// <param name="DbContext">The item database context dependency.</param>
        public BasketModel(IStoreService storeService, ItemDbContext DbContext)
        {
            _storeService = storeService;
            dbContext = DbContext;
        }

        /// <summary>
        /// Called when the Basket page is requested via HTTP GET.
        /// Retrieves the order lines from the store service and assigns them to the _orderLines property.
        /// </summary>
        /// <returns>The IActionResult representing the page result.</returns>
        public IActionResult OnGet()
        {
            _orderLines = _storeService.GetBasket();
            return Page();
        }

        /// <summary>
        /// Called when the "Decrease Amount" button is clicked.
        /// Retrieves the specific order line from the store service and decreases its amount asynchronously.
        /// </summary>
        /// <param name="id">The ID of the order line to decrease the amount.</param>
        /// <returns>the page result.</returns>
        public async Task<IActionResult> OnGetDecreaseAmount(int id)
        {
            OrderLine thisOrderLine = _storeService.GetOrderLine(id);

            if (thisOrderLine != null)
            {
                await _storeService.DecreaseAmount(thisOrderLine, id);
            }

            // Reload the necessary data
            _orderLines = _storeService.GetBasket();

            return Page();
        }

        /// <summary>
        /// Called when the "Increase Amount" button is clicked.
        /// Retrieves the specific order line from the store service and increases its amount asynchronously.
        /// Redirects the user back to the Basket page.
        /// </summary>
        /// <param name="id">The ID of the order line to increase the amount.</param>
        /// <returns>the page result.</returns>
        public async Task<IActionResult> OnGetIncreaseAmount(int id)
        {
            OrderLine thisOrderLine = _storeService.GetOrderLine(id);

            if (thisOrderLine != null)
            {
                await _storeService.IncreaseAmount(thisOrderLine, id);
            }

            return RedirectToPage("/Main/Store/Basket");
        }

        /// <summary>
        /// Called when the form is submitted via HTTP POST.
        /// </summary>
        /// <returns>the page result.</returns>
        public IActionResult OnPostAsync()
        {
            return Page();
        }

        /// <summary>
        /// Calculates the total price of the order lines.
        /// </summary>
        /// <param name="orderLines">The list of order lines.</param>
        /// <returns>The total price of the order lines.</returns>
        public double CalculateTotalPrice(List<OrderLine> orderLines)
        {
            double totalPrice = 0;
            foreach (var orderLine in orderLines)
            {
                totalPrice = Math.Round(totalPrice + orderLine.TotalPrice, 2);
            }
            return totalPrice;
        }

        /// <summary>
        /// Retrieves the product name of an order line.
        /// </summary>
        /// <param name="orderLine">The order line.</param>
        /// <returns>The product name of the order line.</returns>
        public string ProductName(OrderLine orderLine)
        {
            Model.Product product = null;

            if (orderLine.ProductType == "Wool")
            {
                product = dbContext.Wools.FirstOrDefault(p => p.ProductId == orderLine.ProductId);
            }
            else if (orderLine.ProductType == "Yarn")
            {
                product = dbContext.Yarns.FirstOrDefault(p => p.ProductId == orderLine.ProductId);
            }

            return product.ProductName;
        }

        /// <summary>
        /// Retrieves the product image of an order line.
        /// </summary>
        /// <param name="orderLine">The order line.</param>
        /// <returns>The product image of the order line.</returns>
        public string ProductImage(OrderLine orderLine)
        {
            string Image = null;

            if (orderLine.ProductType == "Wool")
            {
                // linq(funktionen) og lambda(anonym funktion "p=> p.___")
                Model.Wool wool = dbContext.Wools.FirstOrDefault(p => p.ProductId == orderLine.ProductId);
                return wool.ImgString;
            }
            else if (orderLine.ProductType == "Yarn")
            {
                Model.Yarn yarn = dbContext.Yarns.FirstOrDefault(p => p.ProductId == orderLine.ProductId);
                return yarn.ImgString;
            }

            return Image;
        }
    }


}
