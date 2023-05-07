using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace RabbitRegister.Model
{

	public enum RabbitRace
	{
		Angora,
		SatinAngora,
	}

	public class Rabbit
	{
		//[Display(Name = "Avlernummer")]
		//[Required(ErrorMessage = "Dit avlsnummer skal udfyldes")]
		//[Range(typeof(int), "4", "4", ErrorMessage = "Dit avlernummer har 4 tal")]
		//[Key]
		//[DatabaseGenerated(DatabaseGeneratedOption.None)]
		//public Breeder BreederRegNo { get; set; }

		[Display(Name = "Kanin ID")]
		[Required(ErrorMessage = "Din kanins skal have et ID")]
		[Range(typeof(int), "1", "9999", ErrorMessage = "Kaninens ID skal være imellem {1} og {2} tal")]
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int RabbitRegNo { get; set; }

		[Display(Name = "Kælenavn: ")]
		[Required(ErrorMessage = "Kaninen skal have et navn"), MaxLength(20)]
		public string Name { get; set; }

	
		[Display(Name = "Race: ")]
		[Required(ErrorMessage = "Kaninen skal have en race")]
		public RabbitRace Race { get; set; }

		[Display(Name = "Farve: ")]
		[Required(ErrorMessage = "Kaninen skal have en farve")]
		public string Color { get; set; }

		//[Display(Name = "Køn (1=dreng): ")]
		//[Required(ErrorMessage = "Skal udfyldes, du kan ændre senere"), MaxLength(2)]
		//public bool Sex { get; set; } = true;

		//[Display(Name = "Fødsels dato: ")]
		//public DateTime DateOfBirth { get; set; }

		//[Display(Name = "Vægt i kilo: ")]
		//[Range(typeof(decimal), minimum: "0", maximum: "10", ErrorMessage = "Vægten må have værdier imellem: {1} og {2}")]
		//public decimal? Weight { get; set; }

		[Display(Name = "Rating: ")]
		[Range(typeof(decimal), minimum: "0", maximum: "100", ErrorMessage = "Kaninens bedømmelse må ligge imellem: {1} og {2}")]
		//[Required(ErrorMessage = "Kaninen skal have en bedømmelse! Du kan ændre den senere")]
		public decimal? Rating { get; set; }


		//[Display(Name = "AveKanin (1=død): ")]
		//public bool IsDead { get; set; } = true;

		//[Display(Name = "Til salg? (1=ja): ")]
		//public bool IsForSale { get; set; } = true;

		//[Display(Name = "Egnet til avl? (beskrivelse): ")]
		//public string? SuitableForBreeding { get; set; }

		//[Display(Name = "Dødsårsag? (beskrivelse): ")]
		//public string? CauseOfDeath { get; set; }



		public Rabbit() { }

		public Rabbit(int rabbitRegNo, string name, RabbitRace race, string color)
		{
			RabbitRegNo = rabbitRegNo;
			Name = name;
			Race = race;
			Color = color;
		}

	}
}
