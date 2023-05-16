using RabbitRegister.Model;

namespace RabbitRegister.Services.BreederService
{
    public interface IBreederService
    {
        public List<Breeder> Breeders { get; }

        Breeder GetBreedByBreederRegNo(int breederRegNo);
        Task AddUserAsync(Breeder breeder);
    }
}
