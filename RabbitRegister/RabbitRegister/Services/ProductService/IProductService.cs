using RabbitRegister.Model;

namespace RabbitRegister.Services.ProductService
{
    public interface IProductService
    {
		Task AddWoolAsync(Wool wool);
		Task<Wool> DeleteWoolAsync(int? Id);
        Task UpdateWoolAsync(Wool wool);
		List<Wool> GetWools();
		Wool GetWools(int id);
	}
}
