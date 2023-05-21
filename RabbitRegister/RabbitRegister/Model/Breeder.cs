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
        public int BreederRegNo { get; set; }
        [Display(Name = "Navn")]
        [Required(ErrorMessage = "Avler Skal Have Navn")]
        public string Name { get; set; }

        [Display(Name = "Adresse")]
        [Required(ErrorMessage = "Avler Skal Have Adresse")]
        public string Adress { get; set; }

        [Display(Name = "Postnummer")]
        [Required(ErrorMessage = "Avler Skal Have Postnummer")]
        [Range(typeof(int), "4", "4", ErrorMessage = "Postnummer skal være på {2} ")]
        public int ZipCode { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Avler Skal Have Email")]
        public string Email { get; set; }

        [Display(Name = "Telefonnumer")]
        [Required(ErrorMessage = "Avler Skal Have Telefonnummer")]
        [Range(typeof(int), "8", "8", ErrorMessage = "Telefonnummer skal være {2} tal")]
        public string Phone {get; set; }
        
        [Required]
        public string Password { get; set; }
        public bool isAdmin { get; set; }

        [ForeignKey(nameof(Rabbit))]
        public int RabbitId { get; set; }
        public virtual Rabbit Rabbit { get; set; }

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
