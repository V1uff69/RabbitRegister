using RabbitRegister.Model;
using RabbitRegister.Services.ProductService;

namespace RabbitRegister.Services.Store
{
    public class StoreService : IStoreService
    {
        private List<Order> _orders;
        private List<OrderLine> _orderLines;

        private DbGenericService<Order> _dbServiceOrder;
        private DbGenericService<OrderLine> _dbServiceOrderLine;
        private IProductService _productService { get; set; }


        public StoreService(DbGenericService<Order> dbServiceOrder, DbGenericService<OrderLine> dbServiceOrderLine, IProductService productService)
        {
            _productService = productService;
            _dbServiceOrder = dbServiceOrder;
            _dbServiceOrderLine = dbServiceOrderLine;
            _orders = dbServiceOrder.GetObjectsAsync().Result.ToList();
            _orderLines = dbServiceOrderLine.GetObjectsAsync().Result.ToList();
        }

        public async Task AddOrderAsync(Order order)
        {
            await _dbServiceOrder.AddObjectAsync(order);
            _orders.Add(order);
        }
        public async Task AddToBasketAsync(int productId)
        {
            // Check if the product is a Wool
            Wool wool = _productService.GetWools(productId);
            if (wool != null)
            {
                OrderLine existingItem = _orderLines.FirstOrDefault(wool => wool.ProductId == productId);

                if (existingItem != null)
                {
                    // If the product is already in the basket, increase the amount
                    existingItem.Amount++;
                    await _dbServiceOrderLine.UpdateObjectAsync(existingItem);
                }
                else
                {
                    OrderLine newOrderline = new OrderLine
                    {
                        ProductId = productId,
                        Amount = 1,
                        Price = wool.Price,
                        Order = null
                    };

                    await _dbServiceOrderLine.AddObjectAsync(newOrderline);
                    _orderLines.Add(newOrderline);
                }

                return;
            }

            // Check if the product is a Yarn
            Yarn yarn = _productService.GetYarn(productId);
            if (yarn != null)
            {
                OrderLine existingItem = _orderLines.FirstOrDefault(yarn => yarn.ProductId == productId);

                if (existingItem != null)
                {
                    // If the product is already in the basket, increase the amount
                    existingItem.Amount++;
                    await _dbServiceOrderLine.UpdateObjectAsync(existingItem);
                }
                else
                {
                    OrderLine newOrderline = new OrderLine
                    {
                        ProductId = productId,
                        Amount = 1,
                        Price = yarn.Price,
                        Order = null
                    };

                    await _dbServiceOrderLine.AddObjectAsync(newOrderline);
                    _orderLines.Add(newOrderline);
                }

                return;
            }
        }
        public List<Order> GetOrders()
        {
            return _orders;
        }

        public List<OrderLine> GetBasket()
        {
            return _orderLines;
        }

        public OrderLine GetOrderLine(int id)
        {
            foreach (OrderLine orderLine in _orderLines)
            {
                if (orderLine.OrderLineId == id)
                {
                    return orderLine;
                }
            }
            return null;
        }

        public async Task DecreaseAmount(OrderLine orderLine, int id)
        {
            OrderLine thisOrderLine = GetOrderLine(id);

            if (thisOrderLine != null)
            {
                thisOrderLine.Amount--;
                await _dbServiceOrderLine.UpdateObjectAsync(thisOrderLine);
                if (thisOrderLine.Amount == 0)
                {
                    await _dbServiceOrderLine.DeleteObjectAsync(thisOrderLine);
                    _orderLines = GetBasket();
                }
            }
        }

        public async Task IncreaseAmount(OrderLine orderLine, int id)
        {
            OrderLine thisOrderLine = GetOrderLine(id);
            if (thisOrderLine != null)
            {
                thisOrderLine.Amount++;
                await _dbServiceOrderLine.UpdateObjectAsync(orderLine);
            }
        }


    }
}
