using Microsoft.AspNetCore.Identity;
using RabbitRegister.Model;

namespace RabbitRegister.MockData
{
    public class MockBreeder
    {
        private static PasswordHasher<string> passwordHasher = new PasswordHasher<string>();

        public static List<Breeder> breeders = new List<Breeder>()
        {
            new Breeder(6969,"Kenni","Roskildevænget __",4000,"kenni.skole@gmail.com","31671600",passwordHasher.HashPassword(null, "Kenni123"),true) {}
        };
        public static List<Breeder> GetMockBreeders() { return breeders; }
    }
}
