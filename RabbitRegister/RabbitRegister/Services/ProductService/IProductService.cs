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

        /// <summary>
        /// Sørger for KUN at brugeren/breederen tilgår Items, den har skabt
        /// </summary>
        /// <param name="breederRegNo">Breederens ID</param>
        /// <returns>Alle </returns>
        List<Wool> GetMyWoolCreations(int breederRegNo);
        List<Yarn> GetMyYarnCreations(int breederRegNo);

		Wool GetWools(int id);
	

        /// <summary>
        /// Yarn
        /// </summary>
        /// <returns></returns>
        List<Yarn> GetYarns();
        Task AddYarnAsync(Yarn yarn);
        void UpdateYarnAsync(Yarn yarn, int id);
        Yarn GetYarn(int yarnId);
        Task<Yarn> DeleteYarnAsync(int? Id);

    }
}
