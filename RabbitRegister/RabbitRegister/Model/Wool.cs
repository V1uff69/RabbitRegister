using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace RabbitRegister.Model
{
    [Table("Wools")]
    public class Wool : Product
    {
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

        public Wool(string productName, double weight, int quality, string color, int breederRegNo, int amount, double price) : base(breederRegNo, productName, color, amount, price)
        {
            WoolId = WoolId++;
            Weight = weight;
            Quality = quality;
            Price = price;
        }
    }
}
