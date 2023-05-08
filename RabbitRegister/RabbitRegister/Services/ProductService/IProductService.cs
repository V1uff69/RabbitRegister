using RabbitRegister.Model;

namespace RabbitRegister.Services.ProductService
{
    public interface IProductService
    {
		Task<Wool> DeleteWoolAsync(int? WoolId);
        Task UpdateWoolAsync(Wool wool);
		List<Wool> GetWools();
		Wool GetWools(int id);
	}
}
