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
			_wools = _dbService.GetObjectsAsync().Result.ToList();
            _dbYarnService = dbYarnService;
            _yarns = dbYarnService.GetObjectsAsync().Result.ToList();

			//_yarns = MockData.MockYarn.GetMockYarns();  //DB tom? Load dette ved første startup.. (NB: Den klarer også _yarns)
			//_wools = MockData.MockWool.GetMockWools();
			//_dbService.SaveObjects(_wools);
		}

        public List<Product> GetProduct(int productId)
		{
			GetWools(productId);
			GetYarn(productId);
			return null;
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
						w.ImgString = wool.ImgString;
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

		public List<Wool> GetMyWoolCreations(int breederRegNo)
		{
			return _wools.Where(wool => wool.BreederRegNo == breederRegNo).ToList();
		}

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
				if (yarn.ProductId == Id)
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

		public Yarn GetYarn(int Id)
		{
			foreach (Yarn yarn in _yarns)
			{
				if (yarn.ProductId == Id)
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
					if (i.ProductId == yarn.ProductId && i.ProductType == yarn.ProductType)
					{
						i.BreederRegNo = yarn.BreederRegNo;
						i.ProductName = yarn.ProductName;
						i.Fiber = yarn.Fiber;
						i.NeedleSize = yarn.NeedleSize;
						i.Length = yarn.Length;
						i.Tension = yarn.Tension;
						i.Washing = yarn.Washing;
						i.Amount = yarn.Amount;
						i.ImgString = yarn.ImgString;
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
