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
        public int OrderId { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public ICollection<OrderLine> OrderLines { get; set; }
        public double? TotalPrice { get; set; }
        [Display(Name = "Name of recipient")]
        [Required(ErrorMessage = "Name is required to check out order"), MinLength(1), MaxLength(50)]
        public string RecipientName { get; set; }
        [Display(Name = "Delivery address")]
        [Required(ErrorMessage = "Delivery address is required to check out order"), MinLength(1), MaxLength(50)]
        public string DeliveryAddress { get; set; }
        [Display(Name = "Name of City")]
        [Required(ErrorMessage = "City is required to check out order"), MinLength(1), MaxLength(50)]
        public string City { get; set; }
        [Display(Name = "Zipcode of City")]
        [Required(ErrorMessage = "Zipcode is required to check out order"), MinLength(4), MaxLength(4)]
        public int ZipCode { get; set; }
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email is required to check out order"), MinLength(1), MaxLength(50)]
        public string Email { get; set; }

    }
}
