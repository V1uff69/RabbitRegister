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

        public BasketModel(IStoreService storeService, ItemDbContext DbContext)
        {
            _storeService = storeService;
            dbContext = DbContext;
        }

        //public async Task<IActionResult> OnGetAddToBasketAsync(int id, string type)
        //{
        //        await _storeService.AddToBasketAsync(id,type);

        //    TempData["Notification"] = "Product added to the basket.";

        //    return RedirectToPage("/Main/Store/Store");
        //}

        public IActionResult OnGet()
        {
            _orderLines = _storeService.GetBasket();
            return Page();
        }

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

        public double CalculateTotalPrice(List<OrderLine> orderLines)
        {
            double totalPrice = 0;
            foreach (var orderLine in orderLines)
            {
                totalPrice = Math.Round(totalPrice + orderLine.TotalPrice,2);
            }
            return totalPrice;
        }

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

        public string ProductImage(OrderLine orderLine)
        {
            string Image = null;

            if (orderLine.ProductType == "Wool")
            {
                //linq(funktionen) og lambda(anonym funktion "p=> p.___)
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
