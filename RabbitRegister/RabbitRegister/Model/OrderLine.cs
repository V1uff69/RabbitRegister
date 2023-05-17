using System.ComponentModel.DataAnnotations;

namespace RabbitRegister.Model
{
    public class OrderLine
    {
        [Key]
        public int OrderLineId { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }
        public double? Price { get; set; }
        public Order Order { get; set; }

    }
}
