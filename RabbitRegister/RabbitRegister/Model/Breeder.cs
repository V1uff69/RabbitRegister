﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RabbitRegister.Model
{
    public class Breeder
    {
        [Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int BreederRegNo { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public int ZipCode { get; set; }
        public string Email { get; set; }
        public string Phone {get; set; }
       
        public Breeder()
        {
        }

        public Breeder(int breederRegNo, string name, string adress, int zipCode, string email, string phone)
        {
            BreederRegNo = breederRegNo;
            Name = name;
            Adress = adress;
            ZipCode = zipCode;
            Email = email;
            Phone = phone;
        }

    }
}
