namespace RabbitRegister.Model
{
    public class Product
    {

        public int ProductId { get; set; }
        public int BreederRegNo { get; set; }
        public string ProductName { get; set; }
        public string Color { get; set; }
        public int amount { get; set; }

        public Product()
        {
        }

        public Product(int productId, int breederRegNo, string productName, string color, int amount)
        {
            ProductId = productId;
            BreederRegNo = breederRegNo;
            ProductName = productName;
            Color = color;
            this.amount = amount;
        }
    }
}
