using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RabbitRegister.Model
{
    [Table("Yarns")] // Angiver tabelnavnet for Yarn-klassen i databasen
    public class Yarn : Product // Yarn-klassen arver fra Product-klassen
    {
        [Display(Name = "Fiber")]
        [Required(ErrorMessage = "Husk at tilføje fiber til garnet ex. Angora 80%, får 20%")]
        public string Fiber { get; set; } // Egenskab der repræsenterer garnets fiberindhold

        [Display(Name = "Nåle Størrelse")]
        [Required(ErrorMessage = "Husk at tilføje størrelse på nål til garnet")]
        [AllowedNeedleSize(ErrorMessage = "Ugyldig nål størrelse ex. 2,5 eller 10")]
        public double NeedleSize { get; set; } // Egenskab der repræsenterer den nålestørrelse, der kræves for garnet

        [Display(Name = "Længde")]
        [Required(ErrorMessage = "Husk at tilføje længde til garnet i meter")]
        public double Length { get; set; } // Egenskab der repræsenterer garnets længde i meter

        [Display(Name = "Strikkefasthed")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Strikkefasthed skal være mellem 1 og 20 tegn")]
        [Required(ErrorMessage = "Husk at tilføje Strikkefasthed til garnet")]
        public string Tension { get; set; } // Egenskab der repræsenterer garnets strikkefasthed

        [Display(Name = "Vaske beskrivelse")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Vaske beskrivelsen skal være mellem 1 og 50 tegn")]
        [Required(ErrorMessage = "Husk at tilføje vaske beskrivelse til garnet")]
        public string Washing { get; set; } // Egenskab der repræsenterer garnets vaskeanvisninger

        public string? ImgString { get; set; } // Egenskab der repræsenterer billedets URL eller streng for garnet (kan være null)

        public Yarn()
        {
            // Standardkonstruktør
        }

        public Yarn(string productType, int breederRegNo, string productName, string fiber, double needleSize, double length, string tension, string washing, string color, int amount, double price, string? imgString) : base(productType, breederRegNo, productName, color, amount, price)
        {
            Fiber = fiber;
            NeedleSize = needleSize;
            Length = length;
            Tension = tension;
            Washing = washing;
            ImgString = "/Images/Products/Yarn/"+imgString;
        }

        public class AllowedNeedleSizeAttribute : ValidationAttribute
        {
            // Definerer en brugerdefineret valideringsattribut kaldet AllowedNeedleSizeAttribute.
            // Denne attribut bruges til at validere nålestørrelsen for garnet.

            private static readonly List<double> AllowedSizes = new List<double>
    {
        // En statisk liste over tilladte nålestørrelser.
        // Disse størrelser bruges til at sammenligne med nålestørrelsen, der skal valideres.
        2, 2.5, 3, 3.5, 4, 4.5, 5, 5.5, 6, 6.5, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20
    };

            protected override ValidationResult IsValid(object value, ValidationContext validationContext)   // En metode, der udfører validering af nålestørrelsen.
            {

                var needleSize = (double)value; // Konverterer værdien til en double og gemmer den i variablen needleSize.

                if (!AllowedSizes.Contains(needleSize))
                {
                    // Hvis nålestørrelsen ikke findes i listen over tilladte størrelser,
                    // betyder det, at den er ugyldig.

                    return new ValidationResult(ErrorMessage);
                    // Returnerer en valideringsfejl med fejlmeddelelsen, der er angivet i attributten.
                }

                return ValidationResult.Success;
                // Hvis nålestørrelsen er gyldig og findes i listen over tilladte størrelser,
                // returneres en succesfuld validering uden fejlmeddelelse.
            }
        }

    }
}
