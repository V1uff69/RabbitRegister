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
        [Required]
		[Range(typeof(int), "1", "9999", ErrorMessage = "Kaninens ID skal være imellem {1} og {2}")]
		public int RabbitRegNo { get; set; }

        [Display(Name = "Avler ID")]
        [Required]
		[Range(typeof(int), "1000", "9999", ErrorMessage = "Avler ID skal være imellem {1} og {2}")]
		public int BreederRegNo { get; set; }

        [Display(Name = "Kaninens Navn")]
        [Required(ErrorMessage = "Der skal angives et Kanin Navn"), MaxLength(20)]
		public string Name { get; set; }

        [Display(Name = "Dato")]
        [Required(ErrorMessage = "Der skal angives en Dato")]
        public DateTime Date { get; set; }

        [Display(Name = "Tid brugt i minutter")]
		[Range(typeof(int), "1", "300", ErrorMessage = "Tid skal være mellem {1} og {2}")]
		public int? TimeUsed { get; set; }

        [Display(Name = "Hårlængde på dag 90")]
		[Range(typeof(float), "1", "15", ErrorMessage = "Hårlængden skal være mellem {1} og {2}")]
		public float? HairLengthByDayNinety { get; set; }

        [Display(Name = "Uld Tæthed")]
		[Range(typeof(float), "1", "20", ErrorMessage = "Uld tætheden skal være mellem {1} og {2}")]
		public float? WoolDensity { get; set; }

        [Display(Name = "1. Sortering vægt i gram")]
        [Required]
		[Range(typeof(int), "0", "1000", ErrorMessage = "Vægten skal være mellem {1} og {2}")]
		public int FirstSortmentWeight { get; set; }

        [Display(Name = "2. Sortering vægt i gram")]
        [Required]
		[Range(typeof(int), "0", "1000", ErrorMessage = "Vægten skal være mellem {1} og {2}")]
		public int SecondSortmentWeight { get; set; }

        [Display(Name = "Kasseret Uld i gram")]
        [Required]
		[Range(typeof(int), "0", "1000", ErrorMessage = "Vægten skal være mellem {1} og {2}")]
		public int DisposableWoolWeight { get; set; }

        public Trimming()
        {
        }

        public Trimming(int rabbitRegNo, int breederRegNo, string name, DateTime date, int timeUsed, int hairLengthByDayNinety, float woolDensity, int firstSortmentWeight, int secondSortmentWeight, int disposableWoolWeight)
        {
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
