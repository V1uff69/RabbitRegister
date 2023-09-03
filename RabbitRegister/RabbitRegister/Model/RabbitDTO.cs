using System.ComponentModel.DataAnnotations;

namespace RabbitRegister.Model
{
    /// <summary>
    /// DATA TRANSFER OBJECT (DTO)
    /// 
    /// Denne DTO-klasse har til formål at fungere som binde-led imellem front-end og class Rabbit.
    /// 
    /// </summary>
    public class RabbitDTO
    {
        [Required(ErrorMessage = "Din kanins skal have et ID")]   // WIP: Vi får ikke denne fejlbesked
        public int RabbitRegNo { get; set; }

        [RegularExpression(@"^\d{4}$", ErrorMessage = "Avler-nr, SKAL bestå af 4 tal!")]
        public int BreederRegNo { get; set; }

        public int Owner { get; set; }

        [Required(ErrorMessage = "Kaninen skal have et navn"), MaxLength(20)]
        public string Name { get; set; }

        public string Race { get; set; }
        public string Color { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DeadOrAlive DeadOrAlive { get; set; }
        public Sex Sex { get; set; }
        public IsForSale? IsForSale { get; set; }
        public string? ImageString { get; set; }


    }
}
