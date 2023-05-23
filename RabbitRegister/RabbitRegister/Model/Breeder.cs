using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RabbitRegister.Model
{
    [Table("Breeder")]
    [PrimaryKey("BreederRegNo")]
    public class Breeder
    {
        [Display(Name = "Avler Id")]
        [RegularExpression(@"^\d{4}$", ErrorMessage = "Avler-ID, SKAL bestå af 4 tal")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BreederRegNo { get; set; }
        [Display(Name = "Navn")]
        [Required(ErrorMessage = "Avler Skal Have Navn")]
        public string Name { get; set; }

        [Display(Name = "Adresse")]
        [Required(ErrorMessage = "Avler Skal Have Adresse")]
        public string Adress { get; set; }

        [Display(Name = "Postnummer")]
        [Required(ErrorMessage = "Avler Skal Have Postnummer")]
        [Range(1000, 9999, ErrorMessage = "Postnummer skal være mellem {1}-{2}.")]
        public int ZipCode { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Avler Skal Have Email")]
        public string Email { get; set; }

        [Display(Name = "Telefonnummer")]
        [Required(ErrorMessage = "Avler Skal Have Telefonnummer")]
        [StringLength(8, MinimumLength = 8, ErrorMessage = "Telefonnummer skal være på {1} cifre.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Telefonnummer skal kun indeholde tal.")]
        public string Phone {get; set; }
        
        [Required]
        public string Password { get; set; }
        public bool isAdmin { get; set; }

        //[ForeignKey(nameof(Rabbit))]
        //public int RabbitId { get; set; }
        //public virtual Rabbit Rabbit { get; set; }

        public Breeder()
        {
        }

        public Breeder(int breederRegNo, string name, string adress, int zipCode, string email, string phone, string password, bool isAdmin)
        {
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
