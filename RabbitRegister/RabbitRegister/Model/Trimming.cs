using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RabbitRegister.Model
{
    public class Trimming
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TrimmingId { get; set; }

        [Display(Name = "Kanin ID")]
        [Required(ErrorMessage = "Der skal angives et Kanin ID")]
        public int RabbitRegNo { get; set; }

        [Display(Name = "Avler ID")]
        [Required(ErrorMessage = "Der skal angives et Avler ID")]
        public int BreederRegNo { get; set; }

        [Display(Name = "Kaninens Navn")]
        public string? Name { get; set; }

        [Display(Name = "Dato")]
        [Required(ErrorMessage = "Der skal angives en Dato")]
        public DateTime Date { get; set; }

        [Display(Name = "Tid brugt i minutter")]
        public double? TimeUsed { get; set; }

        [Display(Name = "Hår længde på dag 90")]
        public double? HairLengthByDayNinety { get; set; }

        [Display(Name = "Uld densitet")]
        public string? WoolDensity { get; set; }

        [Display(Name = "1. Sortering vægt")]
        [Required(ErrorMessage = "Der skal angives 1. Sortering vægt")]
        public double FirstSortmentWeight { get; set; }

        [Display(Name = "2. Sortering vægt")]
        [Required(ErrorMessage = "Der skal angives 2. Sortering vægt")]
        public double SecondSortmentWeight { get; set; }

        [Display(Name = "DisposableWool vægt")]
        [Required(ErrorMessage = "Der skal angives DisposableWool vægt")]
        public double DisposableWoolWeight { get; set; }

        public Trimming()
        {
        }

        public Trimming(int trimmingId, int rabbitRegNo, int breederRegNo, string name, DateTime date, double timeUsed, double hairLengthByDayNinety, string woolDensity, double firstSortmentWeight, double secondSortmentWeight, double disposableWoolWeight)
        {
            TrimmingId = trimmingId;
            RabbitRegNo = rabbitRegNo;
            BreederRegNo = breederRegNo;
            Name = name;
            Date = date;
            TimeUsed = timeUsed;
            HairLengthByDayNinety = hairLengthByDayNinety;
            WoolDensity = woolDensity;
            FirstSortmentWeight = firstSortmentWeight;
            SecondSortmentWeight = secondSortmentWeight;
            DisposableWoolWeight = disposableWoolWeight;
        }
    }
}
