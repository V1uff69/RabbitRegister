using RabbitRegister.Model;

namespace RabbitRegister.Services.Store
{
    public interface IStoreService
    {
        Task AddOrderAsync(Order order);
        Task AddToBasketAsync(int productId, string productType);
        Task DecreaseAmount(OrderLine orderLine, int id);
        Task IncreaseAmount(OrderLine orderLine, int id);
        List<OrderLine> GetBasket();
        OrderLine GetOrderLine(int id);
        List<Order> GetOrders();
    }
}