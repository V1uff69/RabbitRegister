using RabbitRegister.Model;
using RabbitRegister.Services.ProductService;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace RabbitRegister.Services.Store
{
    public class StoreService : IStoreService
    {
        private List<Order> _orders;
        private List<OrderLine> _orderLines = new List<OrderLine>();

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
            GetBasket();
            foreach (OrderLine line in _orderLines) {
                line.OrderId = order.OrderId;
                await _dbServiceOrderLine.AddObjectAsync(line);
                _orderLines.Remove(line);
            }
        }

        public Order GetLastOrder()
        {
            return _orders.OrderByDescending(o => o.OrderId).FirstOrDefault();
        }

        public async Task AddToBasketAsync(int productId, string productType)
        {
            if (productType == "Wool")
            {
                // Check if the product is a Wool
                Wool wool = _productService.GetWools(productId);
                if (wool != null)
                {
                    OrderLine existingWool = _orderLines.FirstOrDefault(wool => wool.ProductId == productId && wool.ProductType == productType);

                    if (existingWool != null)
                    {
                        existingWool.Amount++;
                    }
                    else
                    {
                        OrderLine WoolOrderline = new OrderLine
                        {
                            ProductId = productId,
                            ProductType = productType,
                            Amount = 1,
                            Price = wool.Price,
                            Order = null
                        };
                        _orderLines.Add(WoolOrderline);
                    }

                    return;
                }
            }
            if (productType == "Yarn")
            {
                // Check if the product is a Yarn
                Yarn yarn = _productService.GetYarn(productId);
                if (yarn != null)
                {
                    OrderLine existingYarn = _orderLines.FirstOrDefault(yarn => yarn.ProductId == productId && yarn.ProductType == productType);

                    if (existingYarn != null)
                    {
                        existingYarn.Amount++;
                    }
                    else
                    {

                        OrderLine YarnOrderline = new OrderLine
                        {
                            ProductId = productId,
                            ProductType = productType,
                            Amount = 1,
                            Price = yarn.Price,
                            Order = null
                        };
                        _orderLines.Add(YarnOrderline);
                    }

                    return;
                }
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
                    _orderLines.Remove(thisOrderLine); // Remove the OrderLine from _orderLines
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
