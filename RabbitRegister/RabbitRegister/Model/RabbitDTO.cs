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

        [Display(Name = "Ejer (Avler-ID): ")]
        public int Owner { get; set; }

        [Display(Name = "Kælenavn: ")]
        [Required(ErrorMessage = "Kaninen skal have et navn"), MaxLength(20)]
        public string Name { get; set; }

        [Display(Name = "Race: ")]
        [Required(ErrorMessage = "Kaninen skal have en race"), MaxLength(20)]
        public string Race { get; set; }

        [Display(Name = "Farve: ")]
        [Required(ErrorMessage = "Kaninen skal have en farve"), MaxLength(20)]
        public string Color { get; set; }

        [Display(Name = "Fødsels dato: ")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Vægt i gram: ")]
        [Range(typeof(int), minimum: "150", maximum: "2000", ErrorMessage = "Vægten må have værdier imellem: {1} og {2}")]
        public int? Weight { get; set; }

        [Display(Name = "Bedømmelse: ")]
        [Range(typeof(float), minimum: "0", maximum: "100", ErrorMessage = "Kaninens bedømmelse må ligge imellem: {1} og {2}")]
        public float? Rating { get; set; } = null;

        [Display(Name = "Status (Levende/Død): ")]
        [Required(ErrorMessage = "Der skal oplyses om kaninen er død eller levende")]
        public DeadOrAlive DeadOrAlive { get; set; }

        [Display(Name = "Køn (Han/Hun: ")]
        [Required(ErrorMessage = "Skal udfyldes, du kan ændre senere")]
        public Sex Sex { get; set; }

        [Display(Name = "Til salg? (Ja/Nej): ")]
        public IsForSale? IsForSale { get; set; }

        [Display(Name = "Egnet til avl? (beskrivelse): ")]
        [MaxLength(300)]
        public string? SuitableForBreeding { get; set; } = null;

        [Display(Name = "Dødsårsag? (beskrivelse): ")]
        [MaxLength(300)]
        public string? CauseOfDeath { get; set; } = null;

        [Display(Name = "Yderlige kommentare: ")]
        [MaxLength(300)]
        public string? Comments { get; set; } = null;

        [Display(Name = "Image (Billednavn + type)")]
        public string? ImageString { get; set; }


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
