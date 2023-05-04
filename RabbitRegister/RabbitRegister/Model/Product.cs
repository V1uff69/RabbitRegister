using System.ComponentModel.DataAnnotations;

namespace RabbitRegister.Model
{
    public class Product
    {
        [Key]
        [Display(Name = "ProductId")]
        public int ProductId { get; set; }
        [Display(Name = "BreederRegNo")]
        [Required(ErrorMessage ="Breeder Registration Number is required")]
        public int BreederRegNo { get; set; }
        [Display(Name = "Product name")]
        [Required(ErrorMessage = "Remember to add a product name")]
        public string ProductName { get; set; }
        [Display(Name = "Color")]
        [Required(ErrorMessage = "Remember to add a color")]
        public string Color { get; set; }
        [Display(Name = "Amount")]
        [Required(ErrorMessage = "Remember to add amount")]
        public int amount { get; set; }
        [Display(Name = "Price")]
        [Required(ErrorMessage = "Remember to add price")]
        public int price { get; set; }

        public Product()
        {
        }

        public Product(int productId, int breederRegNo, string productName, string color, int amount, int price)
        {
            ProductId = productId;
            BreederRegNo = breederRegNo;
            ProductName = productName;
            Color = color;
            this.amount = amount;
            this.price = price;
        }
    }
}
