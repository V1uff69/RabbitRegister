using RabbitRegister.Model;

namespace RabbitRegister.Services.Store
{
    public interface IStoreService
    {
        Task AddOrderAsync(Order order);
        Task AddToBasketAsync(int productId, string productType);
        Task DecreaseAmount(int id);
        Task IncreaseAmount(int id);
        List<OrderLine> GetBasket();
        OrderLine GetOrderLine(int id);
        List<Order> GetOrders();
    }
}