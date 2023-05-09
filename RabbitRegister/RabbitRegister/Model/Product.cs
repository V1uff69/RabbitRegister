using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RabbitRegister.Model
{
    [PrimaryKey(nameof(ProductId))]
    public class Product
    {
        [Display(Name = "ProductId")]
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }
        [Display(Name = "ProductType")]
        [Required(ErrorMessage ="Product type is required")]
        public string ProductType { get; set; }
        [Display(Name = "BreederRegNo")]
        [Required(ErrorMessage ="Breeder Registration Number is required")]
        [ForeignKey(nameof(BreederRegNo))]
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

        public Product(string productType, int breederRegNo, string productName, string color, int amount, double price)
        {
            ProductId = ProductId++;
            ProductType = productType;
            BreederRegNo = breederRegNo;
            ProductName = productName;
            Color = color;
            this.Amount = amount;
            Price = price;
            
        }
    }
}
