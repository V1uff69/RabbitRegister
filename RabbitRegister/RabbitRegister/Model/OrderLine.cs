using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RabbitRegister.Model
{
    [Keyless]
    public class OrderLine
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrderLineId { get; set; }
        [ForeignKey(nameof(ProductId))]
        [Required]
        public int ProductId { get; set; }

        [Required]
        public string ProductType { get; set; }

        [Required]
        public int Amount { get; set; }

        [Required]
        public double Price { get; set; }
        [Required]
        public double TotalPrice { get; set; }

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrderId { get; set; }

        [Required]
        public Order? Order { get; set; }
    }
}
