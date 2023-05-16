using RabbitRegister.Model;

namespace RabbitRegister.Services.BreederService
{
    public interface IBreederService
    {
        public List<Breeder> Breeders { get; }

        Breeder GetBreedByBreederRegNo(int breederRegNo);
        Task AddUserAsync(Breeder breeder);
		List<Breeder> GetBreeders();
		Breeder GetBreeder(int breederRegNo);
		void UpdateBreederAsync(Breeder breeder, int breederRegNo);
		Task<Breeder> DeleteBreederAsync(int? breederRegNo);
	}
}
