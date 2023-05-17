using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RabbitRegister.Model;

namespace RabbitRegister.Pages.Main.Store
{
    public class CreateOrderModel : PageModel
    {
        public Order Order { get; set; }

        public CreateOrderModel()
        {
            Order = new Order();
            Order.OrderLine = new List<OrderLine>();
        }
        public void OnGet()
        {
        }
    }
}
