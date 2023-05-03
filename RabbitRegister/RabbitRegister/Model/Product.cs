namespace RabbitRegister.Model
{
    public class Product
    {

        public int ProductId { get; set; }
        public Breeder BreederId { get; set; }
        public string ProductName { get; set; }
        public string Color { get; set; }
        public int amount { get; set; }

        public Product()
        {
        }

        public Product(int productId, string productName, string color, int amount)
        {
            ProductId = productId;
            ProductName = productName;
            Color = color;
            this.amount = amount;
        }


    }
}
