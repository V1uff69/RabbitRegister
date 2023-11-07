using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace RabbitRegister.Model
{
    public enum Sex
    {
        Han,
        Hun,
    }

    public enum DeadOrAlive
    {
        Levende,
        Død,
    }

    public enum IsForSale
    {
        Ja,
        Nej,
    }

    public class Rabbit
    {

        [Key]
        [Column(Order = 0)]
        [Display(Name = "Kanin-ID")]
        [Required(ErrorMessage = "Din kanins skal have et ID")]   // WIP: Vi får ikke denne fejlbesked
        [Range(typeof(int), "1", "9999", ErrorMessage = "Kaninens ID skal være imellem {1} og 4 cifre")]
        public int RabbitRegNo { get; set; }

        [Key]
        [Column(Order = 1)]
        [Display(Name = "Avler-ID")]
        [RegularExpression(@"^\d{4}$", ErrorMessage = "Avler-nr, SKAL bestå af 4 tal!")]
        public int OriginRegNo { get; set; }

        
        [ForeignKey("Breeder")]
        [Display(Name = "Ejer (Avler-ID): ")]
        public int? Owner { get; set; }
        public virtual Breeder Breeder { get; set;} // virtual -> lazy loading (færre DB requests)

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
        [Range(typeof(float), minimum: "150", maximum: "2000", ErrorMessage = "Vægten må have værdier imellem: {1} og {2}")]
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
        //[Required(ErrorMessage = "Der skal oplyses om kaninen er til salg")]
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

        public Rabbit() { }

        public Rabbit(int rabbitRegNo, int originRegNo, int owner, string name, string race, string color, Sex sex, DateTime dateOfBirth, int weight, float? rating, DeadOrAlive deadOrAlive, IsForSale isForSale, string? suitableForBreeding, string? causeOfDeath, string? imageString)
        {
            RabbitRegNo = rabbitRegNo;
            OriginRegNo = originRegNo;
            Owner = owner;
            Name = name;
            Race = race;
            Color = color;
            Sex = sex;
            DateOfBirth = dateOfBirth;
            Weight = weight;
            Rating = rating;
            DeadOrAlive = deadOrAlive;
            IsForSale = isForSale;
            SuitableForBreeding = suitableForBreeding;
            CauseOfDeath = causeOfDeath;
            ImageString = imageString;
        }

        public bool ValidateRace()
        {
            var validRaces = new List<string> { "Angora", "Satin-Angora" };
            return validRaces.Contains(Race);
        }

        
    }
}
