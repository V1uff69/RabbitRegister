using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RabbitRegister.Model;
using RabbitRegister.Services.ProductService;
using RabbitRegister.Services.Store;

namespace RabbitRegister.Pages.Main.Store
{
    public class StoreModel : PageModel
    {
        private IProductService _productService { get; set; }
        private IStoreService _storeService { get; set; }

        public List<Model.Wool> Wools { get; set; }
        public List<Model.Yarn> Yarns { get; set; }
        public Model.Product Product { get; set; }
        public List<Model.OrderLine> OrderLines { get; set; }
        public Model.Order Order { get; set; }

        public StoreModel(IProductService productService, IStoreService storeService)
        {
            _productService = productService;
            _storeService = storeService;
        }
        public void OnGet()
        {
            Yarns = _productService.GetYarns();
            Wools = _productService.GetWools();
            OrderLines = CreateOrderLinesForForm();
        }

        public List<OrderLine> CreateOrderLinesForForm()
        {
            List<OrderLine> tempOrderLines = new List<OrderLine>();
            foreach (var Wool in Wools)
            {
                tempOrderLines.Add(new OrderLine(Wool.ProductId, Wool.ProductType, 0, Wool.Price));
            }
            foreach (var Yarn in Yarns)
            {
                tempOrderLines.Add(new OrderLine(Yarn.ProductId, Yarn.ProductType, 0, Yarn.Price));
            }
            return tempOrderLines;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            /** 
                Først henter vi alle form dataene ud.
            */
            var formData = Request.Form;
            var orderLineAmount = formData["orderLine.Amount"];
            var orderCity = formData["Order.City"];
            // Osv, se form felterne på /Main/Store/Store

            /** 
                Så validere vi felterne så vidt muligt.
                Og opretter OrderLine og Orders udfra formData, og indsætter default værdierne.
            */         

            /** 
                Til sidst gemmer vi OrderLine og Orders i databasen, og redirecter til Checkout
            */

            //var 
            if (!ModelState.IsValid)
            {
                return Page();
            }
            //await _trimmingService.AddTrimmingAsync(trimming);
            return RedirectToPage("GetAllTrimming");
        }

        public async Task<IActionResult> OnPostAddToBasket(int ProductId, string ProductType)
        {
            await _storeService.AddToBasketAsync(ProductId, ProductType);

            TempData["Notification"] = "Product added to the basket.";

            return RedirectToPage("/Main/Store/Store");
        }
    }
}
