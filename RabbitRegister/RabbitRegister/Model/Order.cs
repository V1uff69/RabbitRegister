namespace RabbitRegister.Model
{
    public class Order
    {

        public int OrderId { get; set; }
        public List<OrderLine> OrderLines { get; set; }
        public double TotalPrice { get; set; }
        public string RecipentName { get; set; }
        public string Address { get; set; }
        public int City { get; set; }
        public int ZipCode { get; set; }
        public string Email { get; set; }

        public Order()
        {
        }

        public Order(int orderId, List<OrderLine> orderLines, double totalPrice, string recipentName, string address, int city, int zipCode, string email)
        {
            OrderId = orderId;
            OrderLines = orderLines;
            TotalPrice = totalPrice;
            RecipentName = recipentName;
            Address = address;
            City = city;
            ZipCode = zipCode;
            Email = email;
        }
    }
}
