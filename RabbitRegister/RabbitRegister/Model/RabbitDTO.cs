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
        public int RabbitRegNo { get; set; }
        public int BreederRegNo { get; set; }
        public int Owner { get; set; }

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
