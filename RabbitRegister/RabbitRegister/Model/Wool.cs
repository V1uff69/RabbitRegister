using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace RabbitRegister.Model
{
    [Table("Wools")]
    public class Wool : Product
    {
        [Display(Name = "Vægt (gram): ")]
        [Range(typeof(double), minimum: "0", maximum: "300", ErrorMessage = "Vægten må have værdier imellem: {1} og {2}")]
        public double Weight { get; set; }

        [Display(Name = "Kvalitet (1'st/2'nd sortering)")]
        [Range(typeof(int), minimum: "1", maximum: "2", ErrorMessage = "Du kan kun vælge: {1} eller {2}")]
        public int Quality { get; set; }

        [Display(Name = "Image (Billednavn + type)")]
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
