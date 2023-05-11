using RabbitRegister.MockData;
using RabbitRegister.Model;
using RabbitRegister.Services.JsonFileService;

namespace RabbitRegister.Services.UserService
{
	public class UserService
	{
        public List<User> Users { get; }
        //private JsonFileService<User> _jsonFileService;
        private UserDbService _dbService;

        public UserService(UserDbService dbService)
        {
            //_jsonFileService = jsonFileService;
            _dbService = dbService;
            //Users = MockUsers.GetMockUsers();
            //Users = _jsonFileService.GetJsonObjects().ToList();
            //_jsonFileService.SaveJsonObjects(Users);
            //_dbService.SaveObjects(Users);
            Users = _dbService.GetObjectsAsync().Result.ToList();

        }

        public async Task AddUserAsync(User user)
        {
            Users.Add(user);
            //_jsonFileService.SaveJsonObjects(Users);
            await _dbService.AddObjectAsync(user);
        }
        public User GetUserByUserName(string username)
        {
            //return DbService.GetObjectByIdAsync(username).Result;
            return Users.Find(user => user.UserName == username);
        }
        
    }
}
