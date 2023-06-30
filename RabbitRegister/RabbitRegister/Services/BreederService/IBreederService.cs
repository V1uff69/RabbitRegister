using RabbitRegister.Model;

namespace RabbitRegister.Services.BreederService
{
    public interface IBreederService
    {
        public List<Breeder> Breeders { get; }

        Breeder GetBreedByBreederRegNo(int breederRegNo);
        IEnumerable<Breeder> SearchByBreederRegNo(string breederRegNo);
        IEnumerable<Breeder> SearchByBreederName(string name);
        Task AddUserAsync(Breeder breeder);
        List<Breeder>  GetBreeders();
        Breeder GetBreeder(int breederRegNo);
        Task UpdateBreederAsync(Breeder breeder);
        Task<Breeder> DeleteBreederAsync(int? breederRegNo);
    }
}
