using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using RabbitRegister.Model;

namespace RabbitRegister.MockData
{
	public class MockUsers
	{
        private static PasswordHasher<string> passwordHasher = new PasswordHasher<string>();

        private static List<User> users = new List<User>() {
              new User("admin", passwordHasher.HashPassword(null, "secret")),
              new User("Mads", passwordHasher.HashPassword(null, "Mads123")),
              new User("Nicolai", passwordHasher.HashPassword(null, "Nicolai123"))
        };


        public static List<User> GetMockUsers()
        {
            return users;
        }

    }
}
