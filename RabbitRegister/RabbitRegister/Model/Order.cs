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

        [Display(Name = "Navn på kvitteringen")]
        [Required(ErrorMessage = "Navn på kvitteringen er påkrævet")]
        [StringLength(50, MinimumLength = 1)]
        public string RecipientName { get; set; }

        [Display(Name = "leveringsadresse")]
        [Required(ErrorMessage = "leveringsadresse er påkrævet")]
        [StringLength(50, MinimumLength = 1)]
        public string DeliveryAddress { get; set; }

        [Display(Name = "By navn")]
        [Required(ErrorMessage = "Navnet på din by er påkrævet")]
        [StringLength(50, MinimumLength = 1)]
        public string City { get; set; }

        [Display(Name = "Postnummer")]
        [Required(ErrorMessage = "Postnummer er påkrævet")]
        [Range(1000, 9999)]
        public int ZipCode { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email skal være udfyldt")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Invalid email format")]
        public string Email { get; set; }
    }
}


