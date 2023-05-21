using RabbitRegister.Model;
using RabbitRegister.Services.ProductService;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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

        //public async Task AddOrderAsync(Order order)
        //{
        //    await _dbServiceOrder.AddObjectAsync(order);
        //    _orders.Add(order);
        //}

        public Order GetLastOrder()
        {
            return _orders.OrderByDescending(o => o.OrderId).FirstOrDefault();
        }

        public async Task AddToBasketAsync(int ProductId, string ProductType)
        {
            if (ProductType == "Wool")
            {
                Wool wool = _productService.GetWools(ProductId);

                OrderLine existingItem = _orderLines.FirstOrDefault(orderline => orderline.ProductId == ProductId && orderline.ProductType == ProductType);

                if (existingItem != null)
                {
                    existingItem.Amount++;
                    await _dbServiceOrderLine.UpdateObjectAsync(existingItem);
                }
                else
                {
                    OrderLine newOrderline = new OrderLine
                    {
                        ProductId = ProductId,
                        ProductType = ProductType,
                        Amount = 1,
                        Price = wool.Price,
                    };

                    await _dbServiceOrderLine.AddObjectAsync(newOrderline);
                    _orderLines.Add(newOrderline);
                }
            }

        }
        //public async Task AddToBasketAsync(int ProductId, string ProductType)
        //{
        //    // Check if the product is a Wool
        //    Wool wool = _productService.GetWools(ProductId);
        //    if (wool != null)
        //    {
        //        OrderLine existingItem = _orderLines.FirstOrDefault(wool => wool.ProductId == ProductId);

        //        if (existingItem != null)
        //        {
        //            // If the product is already in the basket, increase the amount
        //            existingItem.Amount++;
        //            await _dbServiceOrderLine.UpdateObjectAsync(existingItem);
        //        }
        //        else
        //        {
        //            OrderLine newOrderline = new OrderLine
        //            {
        //                ProductId = ProductId,
        //                Amount = 1,
        //                Price = wool.Price,
        //            };

        //            await _dbServiceOrderLine.AddObjectAsync(newOrderline);
        //            _orderLines.Add(newOrderline);
        //        }

        //        return;
        //    }

        //    // Check if the product is a Yarn
        //    Yarn yarn = _productService.GetYarn(ProductId);
        //    if (yarn != null)
        //    {
        //        OrderLine existingItem = _orderLines.FirstOrDefault(yarn => yarn.ProductId == ProductId);

        //        if (existingItem != null)
        //        {
        //            // If the product is already in the basket, increase the amount
        //            existingItem.Amount++;
        //            await _dbServiceOrderLine.UpdateObjectAsync(existingItem);
        //        }
        //        else
        //        {
        //            Order order = GetLastOrder();

        //            if (order == null)
        //            {
        //                // Create a new order
        //                order = new Order();
        //                await _dbServiceOrder.AddObjectAsync(order);
        //            }

        //            OrderLine newOrderline = new OrderLine
        //            {
        //                ProductId = ProductId,
        //                Amount = 1,
        //                Price = yarn.Price,
        //                Order = order
        //            };

        //            await _dbServiceOrderLine.AddObjectAsync(newOrderline);
        //            _orderLines.Add(newOrderline);
        //        }

        //        return;
        //    }
        //}
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
