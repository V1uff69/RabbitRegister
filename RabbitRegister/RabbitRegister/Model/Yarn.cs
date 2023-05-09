using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RabbitRegister.Model
{
    [Table("Yarns")]
    public class Yarn : Product
    {
        
        [Display(Name = "YarnId")]
        [Required(ErrorMessage = "Remember to add Id to Yarn")]
        [Range(typeof(int), "0", "10000", ErrorMessage = "Id must be between (1) and (2)")]
        public int YarnId { get; set; }

		[Display(Name = "Fiber")]
		[Required(ErrorMessage = "Remember to add Fiber to Yarn")]
		public string Fiber { get; set; }

		[Display(Name = "Needle Size")]
		[Required(ErrorMessage = "Remember to add Needle Size to Yarn")]
		public double NeedleSize { get; set; }

		[Display(Name = "Length")]
		[Required(ErrorMessage = "Remember to add Length to Yarn")]
		public double Length { get; set; }

		[Display(Name = "Tension")]
		[Required(ErrorMessage = "Remember to add Tension to Yarn")]
		public string Tension { get; set; }

		[Display(Name = "Washing")]
		[Required(ErrorMessage = "Remember to add Washing to Yarn")]
		public string Washing { get; set; }

        public Yarn()
        {
        }

        public Yarn(string productType, int breederRegNo, int yarnId, string productName, string fiber, double needleSize, double length, string tension, string washing, string color, int amount, double price) : base(productType, breederRegNo, productName, color, amount, price)
        {
            YarnId = yarnId;
            Fiber = fiber;
            NeedleSize = needleSize;
            Length = length;
            Tension = tension;
            Washing = washing;
        }
    }
}
