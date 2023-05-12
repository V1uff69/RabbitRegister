using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RabbitRegister.Model
{

    public enum Sex
    {
        Male,
        Female,
    }

    public enum DeadOrAlive
    {
        Alive,
        Dead,
    }

    public enum IsForSale
    {
        Yes,
        No,
    }

    public class Rabbit
    {

        [Display(Name = "Kanin ID")]
        [Required(ErrorMessage = "Din kanins skal have et ID")]
        [Range(typeof(int), "1", "9999", ErrorMessage = "Kaninens ID skal være imellem {1} og {2} tal")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RabbitRegNo { get; set; }

        [Display(Name = "Avler-nr: ")]
        [Required(ErrorMessage = "Avler ID SKAL udfyldes (4 cifre)")]
        [ForeignKey(nameof(BreederRegNo))]
        public int BreederRegNo { get; set; }

        [Display(Name = "Kælenavn: ")]
        [Required(ErrorMessage = "Kaninen skal have et navn"), MaxLength(20)]
        public string Name { get; set; }


        [Display(Name = "Race: ")]
        [Required(ErrorMessage = "Kaninen skal have en race")]
        public string Race { get; set; }

        [Display(Name = "Farve: ")]
        [Required(ErrorMessage = "Kaninen skal have en farve")]
        public string Color { get; set; }

        [Display(Name = "Køn (Male/Female: ")]
        [Required(ErrorMessage = "Skal udfyldes, du kan ændre senere")]
        public Sex Sex { get; set; }

        [Display(Name = "Fødsels dato: ")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Vægt i kilo: ")]
        [Range(typeof(float), minimum: "0", maximum: "10", ErrorMessage = "Vægten må have værdier imellem: {1} og {2}")]
        public float? Weight { get; set; }

        [Display(Name = "Rating: ")]
        [Range(typeof(float), minimum: "0", maximum: "100", ErrorMessage = "Kaninens bedømmelse må ligge imellem: {1} og {2}")]
        public float? Rating { get; set; } = null;

        [Display(Name = "AveKanin (Alive/Dead): ")]
        [Required(ErrorMessage = "Der skal oplyses om kaninen er død eller levende")]
        public DeadOrAlive DeadOrAlive { get; set; }

        [Display(Name = "Til salg? (Yes/No): ")]
        [Required(ErrorMessage = "Der skal oplyses om kaninen er til salg")]
        public IsForSale IsForSale { get; set; }

        [Display(Name = "Egnet til avl? (beskrivelse): ")]
        [MaxLength(300)]
        public string? SuitableForBreeding { get; set; } = null;

        [Display(Name = "Dødsårsag? (beskrivelse): ")]
        [MaxLength(300)]
        public string? CauseOfDeath { get; set; } = null;

        [Display(Name = "Image (Billednavn + type)")]
        public string? ImageString { get; set; }

        public Rabbit() { }

        public Rabbit(int rabbitRegNo, int breederRegNo, string name, string race, string color, Sex sex, DateTime dateOfBirth, float? weight, float? rating, DeadOrAlive deadOrAlive, IsForSale isForSale, string? suitableForBreeding, string? causeOfDeath)
        {
            RabbitRegNo = rabbitRegNo;
            BreederRegNo = breederRegNo;
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
        }
    }
}
