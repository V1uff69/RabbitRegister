using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace RabbitRegister.Model
{
    [Table("Wools")]
    public class Wool : Product
    {
        [Display(Name = "Weight")]
        [Required(ErrorMessage = "Remember to add a weight")]
        public double Weight { get; set; }
        [Display(Name = "Quality")]
        [Required(ErrorMessage = "Remember to add Quality")]
        public int Quality { get; set; }

        public string? ImgString{ get; set; }
 
        public Wool()
        {
        }

        public Wool(string productType, string productName, double weight, int quality, string color, int breederRegNo, int amount, double price, string? imgString) : base(productType, breederRegNo, productName, color, amount, price)
        {
            Weight = weight;
            Quality = quality;
            Price = price;
            ImgString = "/Images/Products/Wool/"+imgString;
        }
    }
}
