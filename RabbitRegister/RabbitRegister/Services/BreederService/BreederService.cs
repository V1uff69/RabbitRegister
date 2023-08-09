using RabbitRegister.MockData;
using RabbitRegister.Model;

namespace RabbitRegister.Services.BreederService
{
    public class BreederService : IBreederService
    {
        public List<Breeder> Breeders { get; set; } // En liste over alle avlere

        public Breeder Breeder { get; set; } // En enkelt avler

        private DbGenericService<Breeder> _dbService; // En generisk database-service

        public BreederService(DbGenericService<Breeder> dbService)
        {
            _dbService = dbService;
            Breeders = _dbService.GetObjectsAsync().Result.ToList();

            //Breeders = MockBreeder.GetMockBreeders(); //DB tom? Ved første Debug kør denne kode, og udkommenter igen derefter
            //foreach (var breeder in Breeders)
            //{
            //    dbService.AddObjectAsync(breeder).Wait();
            //}
        }

        // Tilføj en avler til listen og databasen
        public async Task AddUserAsync(Breeder breeder)
        {
            Breeders.Add(breeder);
            await _dbService.AddObjectAsync(breeder);
        }

        // Find avleren med det specifikke avler id
        public Breeder GetBreedByBreederRegNo(int breederRegNo)
        {
            return Breeders.Find(breeder => breeder.BreederRegNo == breederRegNo);
        }

        public IEnumerable<Breeder> SearchByBreederRegNo(string breederRegNo)
        {
            if (string.IsNullOrEmpty(breederRegNo)) return Breeders;
            return from breeder in Breeders where breeder.BreederRegNo.ToString().Contains(breederRegNo) select breeder;
        }

        public async Task<Breeder> GetBreederByNameAsync(string name)
        {
            return await Task.Run(() => Breeders.FirstOrDefault(breeder => breeder.Name.Equals(name, StringComparison.OrdinalIgnoreCase)));
        }

        public IEnumerable<Breeder> SearchByBreederName(string name)
        {
            if (string.IsNullOrEmpty(name)) return Breeders;
            return from breeder in Breeders where breeder.Name.ToLower().Contains(name.ToLower()) select breeder;
        }

        // Hent alle avlere fra listen
        public List<Breeder> GetBreeders() { return Breeders; }

        // Find avleren med det specifikke avler Id i listen
        public Breeder GetBreeder(int breederRegNo)
        {
            foreach (Breeder b in Breeders)
            {
                if (b.BreederRegNo == breederRegNo)
                    return b;
            }
            return null;
        }

        // Opdaterer en avler i listen og databasen
        public async Task UpdateBreederAsync(Breeder breeder)
        {
            if (breeder != null)
            {
                foreach (Breeder i in Breeders)
                {
                    if (i.BreederRegNo == breeder.BreederRegNo)
                    {
                        // Opdater avlerens informationer
                        i.Name = breeder.Name;
                        i.Adress = breeder.Adress;
                        i.ZipCode = breeder.ZipCode;
                        i.Email = breeder.Email;
                        i.Phone = breeder.Phone;
                        i.Password = breeder.Password;
                        i.isAdmin = breeder.isAdmin;
                        break;
                    }
                }
                await _dbService.UpdateObjectAsync(breeder);
            }
        }

        // Sletter en avler fra listen og databasen
        public async Task<Breeder> DeleteBreederAsync(int? breederRegNo)
        {
            Breeder breederToBeDeleted = null;
            foreach (Breeder breeder in Breeders)
            {
                if (breeder.BreederRegNo == breederRegNo)
                {
                    breederToBeDeleted = breeder;
                    break;
                }
            }

            if (breederToBeDeleted != null)
            {
                Breeders.Remove(breederToBeDeleted);
                await _dbService.DeleteObjectAsync(breederToBeDeleted);
            }
            return breederToBeDeleted;
        }
    }
}
