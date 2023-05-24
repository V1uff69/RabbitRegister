using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RabbitRegister.Model
{
    [Table("Yarns")]
    public class Yarn : Product
    {
		[Display(Name = "Fiber")]
		[Required(ErrorMessage = "Husk at tilføje fiber til garnet ex. Angora 80%, får 20%")]
		public string Fiber { get; set; }

		[Display(Name = "Nåle Størrelse")]
		[Required(ErrorMessage = "Husk at tilføje størrelse på nål til garnet")]
        [AllowedNeedleSize(ErrorMessage = "Ugyldig nål størrelse")]
        public double NeedleSize { get; set; }

		[Display(Name = "Længde")]
		[Required(ErrorMessage = "Husk at tilføje længde til garnet i meter")]
		public double Length { get; set; }

		[Display(Name = "Strikkefasthed")]
		[StringLength(20, MinimumLength = 1, ErrorMessage = "Strikkefasthed skal være mellem 1 og 20 tegn")]
		[Required(ErrorMessage = "Husk at tilføje Strikkefasthed til garnet")]
		public string Tension { get; set; }

		[Display(Name = "Vaske beskrivelse")]
		[StringLength(50, MinimumLength = 1, ErrorMessage = "Vaske beskrivelsen skal være mellem 1 og 50 tegn")]
		[Required(ErrorMessage = "Husk at tilføje vaske beskrivelse til garnet")]
		public string Washing { get; set; }

        public string? ImgString { get; set; }


        public Yarn()
        {
        }

        public Yarn(string productType, int breederRegNo, string productName, string fiber, double needleSize, double length, string tension, string washing, string color, int amount, double price, string imgString) : base(productType, breederRegNo, productName, color, amount, price)
        {
            Fiber = fiber;
            NeedleSize = needleSize;
            Length = length;
            Tension = tension;
            Washing = washing;
            ImgString = "/Images/Products/Yarn/" + imgString;

        }

        public class AllowedNeedleSizeAttribute : ValidationAttribute
        {
            private static readonly List<double> AllowedSizes = new List<double>
        {
            2, 2.5, 3, 3.5, 4, 4.5, 5, 5.5, 6, 6.5, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20
        };

            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                var needleSize = (double)value;

                if (!AllowedSizes.Contains(needleSize))
                {
                    return new ValidationResult(ErrorMessage);
                }

                return ValidationResult.Success;
            }
        }
    }
}

