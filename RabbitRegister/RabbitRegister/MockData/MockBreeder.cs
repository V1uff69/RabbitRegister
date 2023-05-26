using Microsoft.AspNetCore.Identity;
using RabbitRegister.Model;

namespace RabbitRegister.MockData
{
    public class MockBreeder
    {
        private static PasswordHasher<string> passwordHasher = new PasswordHasher<string>();

        public static List<Breeder> breeders = new List<Breeder>()
        {
            new Breeder(5095,"Ida","Fynsvej 14",4060,"Ida.fri.@gmail.com","27586455",passwordHasher.HashPassword(null, "Ida123"),true) {},
            new Breeder(5053,"Maja","Holbækvej __",4300,"MajaHulstroem.@gmail.com","28733085",passwordHasher.HashPassword(null, "Maja123"),false) {},
        };
        public static List<Breeder> GetMockBreeders() { return breeders; }
    }
}
