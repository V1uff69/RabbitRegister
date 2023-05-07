namespace RabbitRegister.Model
{
    public class Wool : Product
    {
        [Display(Name = "WoolId")]
        public int WoolId { get; set; }
        public double Weight { get; set; }
        public int Quality { get; set; }
 
        public Wool()
        {
        }

        public Wool(int woolId, double weight, int quality)
        {
            WoolId = woolId;
            Weight = weight;
            Quality = quality;
        }

        public Wool(int productId, string productName, string color, int amount) : base(productId, productName, color, amount)
        {

        }
    }
}
