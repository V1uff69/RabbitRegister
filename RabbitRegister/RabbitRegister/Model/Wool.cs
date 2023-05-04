namespace RabbitRegister.Model
{
    public class Wool : Product
    {

        public int WoolId { get; set; }
        public double Weight { get; set; }
        public int Quality { get; set; }
 
        public Wool()
        {
        }

        public Wool(int productId, int breederRegNo, int woolId, string productName, double weight, int quality, string color, int amount) : base(productId, breederRegNo, productName, color, amount)
        {
            WoolId = woolId;
            Weight = weight;
            Quality = quality;
        }
    }
}
