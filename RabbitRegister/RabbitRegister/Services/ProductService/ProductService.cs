using RabbitRegister.MockData;
using RabbitRegister.Model;

namespace RabbitRegister.Services.ProductService
{
	public class ProductService : IProductService
	{

		private List<Wool> _wools;
		private List<Yarn> _yarns;

		private DbGenericService<Wool> _dbService;
		private DbGenericService<Yarn> _dbYarnService;

		public ProductService(DbGenericService<Wool> dbService, DbGenericService<Yarn> dbYarnService)
		{
			_dbService = dbService;
			//_wools = MockData.MockWool.GetMockWools();
			_wools = _dbService.GetObjectsAsync().Result.ToList();
            _dbYarnService = dbYarnService;
            //_yarns = MockYarn.GetMockYarns();
            _yarns = dbYarnService.GetObjectsAsync().Result.ToList();
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

		public List<Yarn> GetYarns() { return _yarns; }
		public async Task AddYarnAsync(Yarn yarn)
		{
			await _dbYarnService.AddObjectAsync(yarn);
			_yarns.Add(yarn);
		}

		public async Task<Yarn> DeleteYarnAsync(int? Id)
		{
			Yarn yarnToBeDeleted = null;
			foreach (Yarn yarn in _yarns)
			{
				if (yarn.YarnId == Id)
				{
					yarnToBeDeleted = yarn;
					break;
				}
			}

			if (yarnToBeDeleted != null)
			{
				_yarns.Remove(yarnToBeDeleted);
				await _dbYarnService.DeleteObjectAsync(yarnToBeDeleted);
			}
			return yarnToBeDeleted;
		}

		public Yarn GetYarn(int yarnId)
		{
			foreach (Yarn yarn in _yarns)
			{
				if (yarn.YarnId == yarnId)
					return yarn;
			}
			return null;
		}


		public void UpdateYarnAsync(Yarn yarn, int id)
		{
			if (yarn != null)
			{
				foreach (Yarn i in _yarns)
				{
					if (i.YarnId == id)
					{
						i.BreederRegNo = yarn.BreederRegNo;
						i.ProductName = yarn.ProductName;
						i.Fiber = yarn.Fiber;
						i.NeedleSize = yarn.NeedleSize;
						i.Length = yarn.Length;
						i.Tension = yarn.Tension;
						i.Washing = yarn.Washing;
						i.Amount = yarn.Amount;
						i.Color = yarn.Color;
						i.Price = yarn.Price;
						break;
					}
				}
				_dbYarnService.UpdateObjectAsync(yarn);
			}
		}
	}
}
