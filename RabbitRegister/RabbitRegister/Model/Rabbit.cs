using Microsoft.AspNetCore.Mvc;

namespace RabbitRegister.Model
{
    public class Rabbit
    {

        public Breeder BreederRegNo { get; set; }
        public int RabbitRegNo { get; set; }
        public string Name { get; set; }
        public string Race { get; set; }
        public double Rating { get; set; }
        public string Sex { get; set; }
        public bool Breedable { get; set; } 
        
        public Rabbit()
        {
        }

        public Rabbit(Breeder breederRegNo, int rabbitRegNo, string name, string race, double rating, string sex, bool breedable)
        {
            BreederRegNo = breederRegNo;
            RabbitRegNo = rabbitRegNo;
            Name = name;
            Race = race;
            Rating = rating;
            Sex = sex;
            Breedable = breedable;
        }
    }
}
