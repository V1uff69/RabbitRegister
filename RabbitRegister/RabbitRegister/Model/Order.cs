using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace RabbitRegister.Model
{
    [Table("Order")]
    [PrimaryKey("OrderId")]
    public class Order
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public double? TotalPrice { get; set; }

        [Display(Name = "Name of recipient")]
        [Required(ErrorMessage = "Name is required to check out order")]
        [StringLength(50, MinimumLength = 1)]
        public string RecipientName { get; set; }

        [Display(Name = "Delivery address")]
        [Required(ErrorMessage = "Delivery address is required to check out order")]
        [StringLength(50, MinimumLength = 1)]
        public string DeliveryAddress { get; set; }

        [Display(Name = "Name of City")]
        [Required(ErrorMessage = "City is required to check out order")]
        [StringLength(50, MinimumLength = 1)]
        public string City { get; set; }

        [Display(Name = "Zipcode of City")]
        [Required(ErrorMessage = "Zipcode is required to check out order")]
        [Range(1000, 9999)]
        public int ZipCode { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email is required to check out order")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Invalid email format")]
        public string Email { get; set; }
    }
}


