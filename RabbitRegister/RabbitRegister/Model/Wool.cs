using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace RabbitRegister.Model
{
    public class Wool : Product
    {
        //[Key]
        [Display(Name = "WoolId")]
        public int WoolId { get; set; }
        [Display(Name = "Weight")]
        [Required(ErrorMessage = "Remember to add a weight")]
        public double Weight { get; set; }
        [Display(Name = "Quality")]
        [Required(ErrorMessage = "Remember to add Quality")]
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
