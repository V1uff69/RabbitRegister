using RabbitRegister.Model;

namespace RabbitRegister.Services.ProductService
{
    public interface IProductService
    {
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
        List<Yarn> GetYarns();
        Task AddYarnAsync(Yarn yarn);
        void UpdateYarnAsync(Yarn yarn, int id);
        Yarn GetYarn(int yarnId);
        Task<Yarn> DeleteYarnAsync(int? Id);

    }
}
