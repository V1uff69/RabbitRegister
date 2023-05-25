using RabbitRegister.Model;
using RabbitRegister.Services.ProductService;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace RabbitRegister.Services.Store
{
    public class StoreService : IStoreService
    {

        private int nextLineId = 0;

        private List<Order> _orders;
        private List<OrderLine> _orderLines = new List<OrderLine>();

        private DbGenericService<Order> _dbServiceOrder;
        private DbGenericService<OrderLine> _dbServiceOrderLine;
        private IProductService _productService { get; set; }

        /// <summary>
        /// Initializes a new instance of the StoreService class.
        /// </summary>
        /// <param name="dbServiceOrder">The database service for orders.</param>
        /// <param name="dbServiceOrderLine">The database service for orderlines.</param>
        /// <param name="productService">The product service.</param>
        public StoreService(DbGenericService<Order> dbServiceOrder, DbGenericService<OrderLine> dbServiceOrderLine, IProductService productService)
        {
            _productService = productService;
            _dbServiceOrder = dbServiceOrder;
            _dbServiceOrderLine = dbServiceOrderLine;
            _orders = dbServiceOrder.GetObjectsAsync().Result.ToList();
            _orderLines = dbServiceOrderLine.GetObjectsAsync().Result.ToList();
        }

        /// <summary>
        /// Adds an order to the database and updates the associated order lines.
        /// </summary>
        /// <param name="order">The order to add.</param>
        public async Task AddOrderAsync(Order order)
        {
            await _dbServiceOrder.AddObjectAsync(order);
            _orders.Add(order);
            GetBasket();
            double TotalPrice = 0;
            foreach (OrderLine line in _orderLines)
            {
                TotalPrice += line.TotalPrice;
                line.OrderId = order.OrderId;
                await _dbServiceOrderLine.AddObjectAsync(line);
            }
            order.TotalPrice = Math.Round(TotalPrice, 2);
            await _dbServiceOrder.UpdateObjectAsync(order);
            _orders.Clear();
            _orderLines.Clear();
        }

        /// <summary>
        /// Retrieves the last order from the list of orders.
        /// </summary>
        /// <returns>The last order.</returns>
        public Order GetLastOrder()
        {
            return _orders.OrderByDescending(o => o.OrderId).FirstOrDefault();
        }

        /// <summary>
        /// Adds a product to the shopping basket.
        /// </summary>
        /// <param name="productId">The ID of the product to add.</param>
        /// <param name="productType">The type of the product to add.</param>
        /// <returns>nothing as this method used to save directly to db.</returns>
        public async Task AddToBasketAsync(int productId, string productType)
        {
            // If the product type is "Wool", perform the following actions.
            if (productType == "Wool")
            {
                // Retrieve the Wool product from the ProductService based on the provided productId.
                Wool wool = _productService.GetWools(productId);

                // If the Wool product exists.
                if (wool != null)
                {
                    // Check if an orderline for the Wool product already exists in the shopping basket.
                    OrderLine existingWool = _orderLines.FirstOrDefault(wool => wool.ProductId == productId && wool.ProductType == productType);

                    // If an orderline for the Wool product exists, update the quantity and total price.
                    if (existingWool != null)
                    {
                        existingWool.Amount++;
                        existingWool.TotalPrice = Math.Round(existingWool.Price * existingWool.Amount, 2); //calculates the total price of the orderline and rounding to 2 decimal.
                    }
                    // If no orderline for the Wool product exists, create a new order line and add it to the shopping basket.
                    else
                    {
                        OrderLine WoolOrderline = new OrderLine
                        {
                            OrderLineId = nextLineId + 1,
                            ProductId = productId,
                            ProductType = productType,
                            Amount = 1,
                            Price = wool.Price,
                            TotalPrice = wool.Price,
                            Order = null
                        };
                        nextLineId++;
                        _orderLines.Add(WoolOrderline);
                    }
                    return;
                }
            }

            // If the product type is "Yarn", perform the following actions.
            if (productType == "Yarn")
            {
                // Retrieve the Yarn product from the ProductService based on the provided productId.
                Yarn yarn = _productService.GetYarn(productId);

                // If the Yarn product exists.
                if (yarn != null)
                {
                    // Check if an orderline for the Yarn product already exists in the shopping basket using lambda.
                    OrderLine existingYarn = _orderLines.FirstOrDefault(yarn => yarn.ProductId == productId && yarn.ProductType == productType);

                    // If an orderline for the Yarn product exists, update the quantity and total price.
                    if (existingYarn != null)
                    {
                        existingYarn.Amount++;
                        existingYarn.TotalPrice = Math.Round(existingYarn.Price * existingYarn.Amount, 2); //calculates the total price of the orderline and rounding to 2 decimal.
                    }
                    // If no order line for the Yarn product exists, create a new order line and add it to the shopping basket.
                    else
                    {
                        OrderLine YarnOrderline = new OrderLine
                        {
                            OrderLineId = nextLineId + 1,
                            ProductId = productId,
                            ProductType = productType,
                            Amount = 1,
                            Price = yarn.Price,
                            TotalPrice = yarn.Price,
                            Order = null
                        };
                        nextLineId++;
                        _orderLines.Add(YarnOrderline);
                    }
                    return;
                }
            }
        }

        /// <summary>
        /// Retrieves the list of orders.
        /// </summary>
        /// <returns>The list of orders.</returns>
        public List<Order> GetOrders()
        {
            return _orders;
        }

        /// <summary>
        /// Retrieves the shopping basket (list of order lines).
        /// </summary>
        /// <returns>The shopping basket.</returns>
        public List<OrderLine> GetBasket()
        {
            return _orderLines;
        }

        /// <summary>
        /// Retrieves an order line based on its ID.
        /// </summary>
        /// <param name="id">The ID of the order line.</param>
        /// <returns>The order line with the specified ID, or null if not found.</returns>
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

        /// <summary>
        /// Decreases the amount (quantity) of an order line.
        /// </summary>
        /// <param name="orderLine">The order line to decrease the amount for.</param>
        /// <param name="id">The ID of the order line.</param>
        public async Task DecreaseAmount(OrderLine orderLine, int id)
        {
            OrderLine thisOrderLine = GetOrderLine(id);

            if (thisOrderLine != null)
            {
                thisOrderLine.Amount--;
                thisOrderLine.TotalPrice = Math.Round(thisOrderLine.Price * thisOrderLine.Amount, 2); //calculates the total price of the orderline and rounding to 2 decimal.
                if (thisOrderLine.Amount == 0)
                {
                    _orderLines.Remove(thisOrderLine); // Remove the OrderLine from _orderLines
                }
            }
        }

        /// <summary>
        /// Increases the amount (quantity) of an order line.
        /// </summary>
        /// <param name="orderLine">The order line to increase the amount for.</param>
        /// <param name="id">The ID of the order line.</param>
        public async Task IncreaseAmount(OrderLine orderLine, int id)
        {
            OrderLine thisOrderLine = GetOrderLine(id);
            if (thisOrderLine != null)
            {
                thisOrderLine.Amount++;
                thisOrderLine.TotalPrice = Math.Round ( thisOrderLine.Price * thisOrderLine.Amount,2); //calculates the total price of the orderline and rounding to 2 decimal.
            }
        }


    }
}
