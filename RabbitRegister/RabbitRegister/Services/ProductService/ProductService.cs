using RabbitRegister.Model;

namespace RabbitRegister.Services.ProductService
{
    public class ProductService : IProductService
    {
		private List<Wool> _wools;

		private DbGenericService<Wool> _dbService;

		public ProductService(DbGenericService<Wool> dbService)
		{
			_wools = MockData.MockWool.GetMockWools();
			//_wools = _dbService.GetObjectsAsync().Result.ToList();
		}

		public List<Wool> GetWools() { return _wools; }
	}
}
