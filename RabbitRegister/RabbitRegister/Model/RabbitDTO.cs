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
        public int OriginRegNo { get; set; }

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

        // Definer en metode for at validere farven
        //public bool ValidateColor()
        //{
        //    // Opret en dictionary, der mapper racer til gyldige farver
        //    var colorsByRace = new Dictionary<string, List<string>>
        //    {
        //        ["Angora"] = new List<string> { "Grå", "Gul", "Blå", "Hvid", "Sort" },
        //        ["Satin-Angora"] = new List<string> { "Beige", "Sort", "Rød", "Brun" }
        //    };

        //    // Kontroller, om den valgte farve er gyldig for den valgte race
        //    if (colorsByRace.ContainsKey(Race))
        //    {
        //        return colorsByRace[Race].Contains(Color);
        //    }

        //    return false;
        //}


        public bool Validate()
        {
            return ValidateRace() &&
                   ValidateColor();
        }

        private bool ValidateRace()
        {
            var validRaces = new List<string> { "Angora", "Satin-Angora" };
            return validRaces.Contains(Race);
        }

        private bool ValidateColor()
        {
            // Opret en dictionary, der mapper racer til gyldige farver
            var colorsByRace = new Dictionary<string, List<string>>
            {
                ["Angora"] = new List<string> { "Grå", "Gul", "Blå", "Hvid", "Sort" },
                ["Satin-Angora"] = new List<string> { "Beige", "Sort", "Rød", "Brun" }
            };

            // Kontroller, om den valgte farve er gyldig for den valgte race
            if (colorsByRace.ContainsKey(Race))
            {
                return colorsByRace[Race].Contains(Color);
            }

            return false;
        }
    }
}
