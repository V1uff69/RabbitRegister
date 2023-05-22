using RabbitRegister.MockData;
using RabbitRegister.Model;

namespace RabbitRegister.Services.BreederService
{
    public class BreederService : IBreederService
    {
        public List<Breeder> Breeders { get; set; }
        public Breeder Breeder { get; set; }
        private DbGenericService<Breeder> _dbService;

        public BreederService(DbGenericService<Breeder> dbService)
        {
            _dbService = dbService;
            Breeders = _dbService.GetObjectsAsync().Result.ToList();

            Breeders = MockBreeder.GetMockBreeders();
            _dbService.SaveObjects(Breeders);
        }

        public async Task AddUserAsync(Breeder breeder)
        {
            Breeders.Add(breeder);
            await _dbService.AddObjectAsync(breeder);
        }
        public Breeder GetBreedByBreederRegNo(int breederRegNo)
        {
            //return DbService.GetObjectByIdAsync(username).Result;
            return Breeders.Find(breeder => breeder.BreederRegNo == breederRegNo);
        }
    }
}
