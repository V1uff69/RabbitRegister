using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RabbitRegister.Model
{
    [Table("Product")]
    [PrimaryKey("ProductId")]
    public class Product
    {
        [Display(Name = "ProductId")]
        public int ProductId { get; set; }
        [Display(Name = "BreederRegNo")]
        [Required(ErrorMessage ="Breeder Registration Number is required")]
        //[ForeignKey]
        public int BreederRegNo { get; set; }
        [Display(Name = "Product name")]
        [Required(ErrorMessage = "Remember to add a product name")]
        public string ProductName { get; set; }
        [Display(Name = "Color")]
        [Required(ErrorMessage = "Remember to add a color")]
        public string Color { get; set; }
        [Display(Name = "Amount")]
        [Required(ErrorMessage = "Remember to add amount")]
        public int Amount { get; set; }
        [Display(Name ="Price")]
        [Required(ErrorMessage = "Remember to add price")]
        public double Price { get; set; }

        public Product()
        {
        }

        public Product(int breederRegNo, string productName, string color, int amount, double price)
        {
            ProductId = ProductId++;
            BreederRegNo = breederRegNo;
            ProductName = productName;
            Color = color;
            this.Amount = amount;
            Price = price;
            
        }
    }
}
