using RabbitRegister.Model;

namespace RabbitRegister.Services.ProductService
{
    public interface IProductService
    {
        List<Product> GetProduct(int productId);
        /// <summary>
        /// Wool
        /// </summary>
        /// <param name="wool"></param>
        /// <returns></returns>
        Task AddWoolAsync(Wool wool);
		Task<Wool> DeleteWoolAsync(int? Id);
        Task UpdateWoolAsync(Wool wool);
		List<Wool> GetWools();
		Wool GetWools(int id);


        /// <summary>
        /// Yarn
        /// </summary>
        /// <returns></returns>
        List<Yarn> GetYarns(); // Returnerer alle garn-objekter
        Task AddYarnAsync(Yarn yarn); // Tilføjer garn til databasen og den interne liste
        void UpdateYarnAsync(Yarn yarn, int id); // Opdaterer garn i databasen og den interne liste
        Yarn GetYarn(int yarnId); // Returnerer et specifikt garn-objekt baseret på id
        Task<Yarn> DeleteYarnAsync(int? Id); // Sletter garn fra databasen og den interne liste
    }
}
