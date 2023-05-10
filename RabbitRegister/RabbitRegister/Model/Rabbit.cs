using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

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

		//[Display(Name = "Avlernummer")]
		//[Required(ErrorMessage = "Dit avlsnummer skal udfyldes")]
		//[Range(typeof(int), "1", "9999", ErrorMessage = "Dit avlernummer har 4 tal")]
		//[ForeignKey(nameof(BreederRegNo))]
		//public Breeder BreederRegNo { get; set; }


		[Display(Name = "Kælenavn: ")]
		[Required(ErrorMessage = "Kaninen skal have et navn"), MaxLength(20)]
		public string Name { get; set; }

		public static readonly List<string> PossibleRaces = new List<string>
		{
		"Angora",
		"Satin Angora"
		};

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
		[Range(typeof(decimal), minimum: "0", maximum: "10", ErrorMessage = "Vægten må have værdier imellem: {1} og {2}")]
		public decimal? Weight { get; set; }

		[Display(Name = "Rating: ")]
		[Range(typeof(decimal), minimum: "0", maximum: "100", ErrorMessage = "Kaninens bedømmelse må ligge imellem: {1} og {2}")]
		//[Required(ErrorMessage = "Kaninen skal have en bedømmelse! Du kan ændre den senere")]
		public decimal? Rating { get; set; } = null;

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


		public Rabbit() { }

		public Rabbit(int rabbitRegNo, string name, string race, string color, Sex sex, DateTime dateOfBirth, decimal? weight, decimal? rating, DeadOrAlive deadOrAlive, IsForSale isForSale, string? suitableForBreeding, string? causeOfDeath)
		{
			RabbitRegNo = rabbitRegNo;
			//BreederRegNo = breederRegNo;
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
