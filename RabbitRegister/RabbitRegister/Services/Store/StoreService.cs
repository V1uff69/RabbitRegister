using RabbitRegister.Model;

namespace RabbitRegister.Services.Store
{
    public class StoreService : IStoreService
    {
        private List<Order> _orders;
        private List<OrderLine> _orderLines;
        private DbGenericService<Order> _dbServiceOrder;
        private DbGenericService<OrderLine> _dbServiceOrderLine;

        public StoreService(DbGenericService<Order> dbServiceOrder, DbGenericService<OrderLine> dbServiceOrderLine)
        {
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
        public async Task AddToBasketAsync(OrderLine orderLine)
        {
            OrderLine existingItem = _orderLines.FirstOrDefault(item => item.ProductId == orderLine.ProductId);

            if (existingItem != null)
            {
                OrderLine newOrderline = new OrderLine
                { 
                    ProductId = orderLine.ProductId,
                    Amount = orderLine.Amount,
                    Price = orderLine.Price,
                    Order = orderLine.Order,
                };
                // If the product is already in the basket, increase the amount
                existingItem.Amount++;
                await _dbServiceOrderLine.UpdateObjectAsync(existingItem);
            }
            else
            {
                await _dbServiceOrderLine.AddObjectAsync(orderLine);
                _orderLines.Add(orderLine);
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
            if (thisOrderLine.Amount == 0)
            {
                OrderLine ToBeDeleted = null;
                foreach (OrderLine ol in _orderLines)
                {
                    if (ol.ProductId == id)
                    {
                        ToBeDeleted = ol;
                        break;
                    }
                }
                if (ToBeDeleted != null)
                {
                    _orderLines.Remove(ToBeDeleted);
                    await _dbServiceOrderLine.DeleteObjectAsync(ToBeDeleted);
                }
            }
            else
            if (thisOrderLine != null)
            {
                thisOrderLine.Amount--;
                await _dbServiceOrderLine.UpdateObjectAsync(thisOrderLine);
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
