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
			_dbService = dbService;
			//_wools = MockData.MockWool.GetMockWools();
			_wools = _dbService.GetObjectsAsync().Result.ToList();
		}


		public async Task AddWoolAsync(Wool wool)
		{
			await _dbService.AddObjectAsync(wool);
			_wools.Add(wool);
		}

		public async Task UpdateWoolAsync(Wool wool)
		{
			if (wool != null)
			{
				foreach (Wool w in _wools)
				{
					if (w.ProductId == wool.ProductId && w.ProductType == wool.ProductType)
					{
						w.ProductName = wool.ProductName;
						w.Weight = wool.Weight;
						w.Quality = wool.Quality;
						w.Color = wool.Color;
						w.BreederRegNo = wool.BreederRegNo;
						w.Amount = wool.Amount;
						w.Price = wool.Price;
						break;
					}
				}
				await _dbService.UpdateObjectAsync(wool);
			}
		}

		public async Task<Wool> DeleteWoolAsync(int? Id)
		{
			Wool woolToBeDeleted = null;
			foreach (Wool w in _wools)
			{
				if (w.ProductId == Id)
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
				if (w.ProductId == id)
					return w;
			}
			return null;
		}
	}
}
