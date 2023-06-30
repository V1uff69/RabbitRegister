using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RabbitRegister.Model
{
    [Table("Breeder")] // Angiver tabellens navn i databasen
    [PrimaryKey("BreederRegNo")] // Angiver primærnøglen for tabellen
    public class Breeder
    {
        [Display(Name = "Avler-ID")]
        [RegularExpression(@"^\d{4}$", ErrorMessage = "Avler-ID, SKAL bestå af 4 tal")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] // Angiver, at værdien ikke genereres automatisk af databasen
        public int BreederRegNo { get; set; } // Avlerens registreringsnummer

        [Display(Name = "Navn")]
        [Required(ErrorMessage = "Avler Skal Have Navn")] // Feltet er påkrævet
        public string Name { get; set; } // Avlerens navn

        [Display(Name = "Adresse")]
        [Required(ErrorMessage = "Avler Skal Have Adresse")]
        public string Adress { get; set; } // Avlerens adresse

        [Display(Name = "Postnummer")]
        [Required(ErrorMessage = "Avler Skal Have Postnummer")]
        [Range(1000, 9999, ErrorMessage = "Postnummer skal være mellem {1}-{2}.")] // Angiver et numerisk interval for værdien
        public int ZipCode { get; set; } // Avlerens postnummer

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Avler Skal Have Email")]
        public string Email { get; set; } // Avlerens email

        [Display(Name = "Telefonnummer")]
        [Required(ErrorMessage = "Avler Skal Have Telefonnummer")]
        [StringLength(8, MinimumLength = 8, ErrorMessage = "Telefonnummer skal være på {1} cifre.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Telefonnummer skal kun indeholde tal.")]
        public string Phone { get; set; } // Avlerens telefonnummer

        [Display(Name = "Adgangskode")]
        [Required(ErrorMessage = "Adgangkoden skal udfyldes for bekræftning")]
        public string Password { get; set; } // Avlerens adgangskode

        public bool isAdmin { get; set; } // Angiver om avleren er administrator

        // [ForeignKey(nameof(Rabbit))] 
        // public int RabbitId { get; set; }
        // public virtual Rabbit Rabbit { get; set; }

        public Breeder()
        {
            // Tom constructor
        }

        public Breeder(int breederRegNo, string name, string adress, int zipCode, string email, string phone, string password, bool isAdmin)
        {
            // Constructor med properties for at oprette en ny avler
            BreederRegNo = breederRegNo;
            Name = name;
            Adress = adress;
            ZipCode = zipCode;
            Email = email;
            Phone = phone;
            Password = password;
            this.isAdmin = isAdmin;
        }
    }
}
