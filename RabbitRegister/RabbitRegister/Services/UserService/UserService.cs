using RabbitRegister.MockData;
using RabbitRegister.Model;

namespace RabbitRegister.Services.UserService
{
	public class UserService
	{
        public List<User> Users { get; }
        private UserDbService _dbService;

        public UserService(UserDbService dbService)
        {
            _dbService = dbService;            
            Users = _dbService.GetObjectsAsync().Result.ToList();

            //Users = MockUsers.GetMockUsers();
            //_dbService.SaveObjects(Users);

        }

        public async Task AddUserAsync(User user)
        {
            Users.Add(user);
            await _dbService.AddObjectAsync(user);
        }
        public User GetUserByUserName(string username)
        {
            //return DbService.GetObjectByIdAsync(username).Result;
            return Users.Find(user => user.UserName == username);
        }
        
    }
}
