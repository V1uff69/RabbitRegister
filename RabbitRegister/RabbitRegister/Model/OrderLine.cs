using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RabbitRegister.Model
{
    public class OrderLine
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderLineId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public int ProductId { get; set; }
        public string ProductType { get; set; }
        public int Amount { get; set; }
        public double Price { get; set; }
        public Order? Order { get; set; }

        public OrderLine()
        {
        }

        public OrderLine(int productId, string productType, int amount, double price)
        {
            ProductId = productId;
            ProductType = productType;
            Amount = amount;
            Price = price;
        }
    }
}
