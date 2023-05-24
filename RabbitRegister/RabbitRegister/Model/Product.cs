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

        [Display(Name = "Produkt Type")]
        [Required(ErrorMessage ="Produkt type skal indtastes")]
        public string ProductType { get; set; }

        [Display(Name = "Avler ID")]
        [Required(ErrorMessage ="Avler ID er nødvendigt")]
        [ForeignKey(nameof(BreederRegNo))]
        public int BreederRegNo { get; set; }

        [Display(Name = "Produkt Navn")]
        [Required(ErrorMessage = "Husk at tilføje navn til produktet")]
        public string ProductName { get; set; }

        [Display(Name = "Farve")]
        [Required(ErrorMessage = "Husk at tilføje farve til produktet")]
        public string Color { get; set; }
        [Display(Name = "Antal")]

        [Required(ErrorMessage = "Husk at angiv antal til produktet")]
        public int Amount { get; set; }
        [Display(Name ="Pris")]

        [Required(ErrorMessage = "Husk at tilføje pris til produktet")]
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
