using RabbitRegister.Model;

namespace RabbitRegister.Services.ProductService
{
	public class ProductService : IProductService
	{


		/// <summary>
		/// Wool Service
		/// </summary>
		private List<Wool> _wools;

		private DbGenericService<Wool> _dbService;

		public ProductService(DbGenericService<Wool> dbService)
		{
			_wools = MockData.MockWool.GetMockWools();
			//_wools = _dbService.GetObjectsAsync().Result.ToList();
		}


		public async Task AddWoolAsync(Wool wool)
		{
			_wools.Add(wool);
			await _dbService.AddObjectAsync(wool);
		}

		public async Task UpdateWoolAsync(Wool wool)
		{
			if (wool != null)
			{
				foreach (Wool w in _wools)
				{
					if (w.WoolId == wool.WoolId)
					{
						w.ProductName = wool.ProductName;
						w.Price = wool.Price;
					}
				}
				await _dbService.UpdateObjectAsync(wool);
			}
		}

		public async Task<Wool> DeleteWoolAsync(int? WoolId)
		{
			Wool woolToBeDeleted = null;
			foreach (Wool w in _wools)
			{
				if (w.WoolId == WoolId)
				{
					woolToBeDeleted = w;
					break;
				}
			}
			if (woolToBeDeleted != null)
			{
				_wools.Remove(woolToBeDeleted);
				await _dbService.DeleteObjectAsync(woolToBeDeleted);
			}

			return woolToBeDeleted;
		}
		public List<Wool> GetWools() { return _wools; }

		public Wool GetWools(int id)
		{
			foreach (Wool w in _wools)
			{
				if (w.WoolId == id)
					return w;
			}
			return null;
		}
	}
}
