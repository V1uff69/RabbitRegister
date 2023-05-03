namespace RabbitRegister.Model
{
    public class OrderLine
    {

        public int Id { get; set; }
        public Product Product { get; set; }
        public int Amount { get; set; }
        public double Price { get; set; }

        public OrderLine()
        {
        }

        public OrderLine(int id, Product product, int amount, double price)
        {
            Id = id;
            Product = product;
            Amount = amount;
            Price = price;
        }
    }
}
