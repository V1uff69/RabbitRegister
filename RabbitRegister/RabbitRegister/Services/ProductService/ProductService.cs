﻿using RabbitRegister.MockData;
using RabbitRegister.Model;

namespace RabbitRegister.Services.ProductService
{
	public class ProductService : IProductService
	{
		private List<Wool> _wools;
		private List<Yarn> _yarns;

		private DbGenericService<Wool> _dbService;
		private DbGenericService<Yarn> _dbYarnService;

		public ProductService(DbGenericService<Wool> dbService)
		{
			_wools = _dbService.GetObjectsAsync().Result.ToList();
		}
		public ProductService(DbGenericService<Yarn> dbYarnService)
		{
			_dbYarnService = dbYarnService;
			//_yarns = MockYarn.GetMockYarns();
			_yarns = dbYarnService.GetObjectsAsync().Result.ToList();

		}
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


		public void UpdateYarnAsync(Yarn yarn)
		{
			if (yarn != null)
			{
				foreach (Yarn i in _yarns)
				{
					if (i.YarnId == yarn.YarnId)
					{
						i.BreederRegNo = yarn.BreederRegNo;
						i.ProductName = yarn.ProductName;
						i.Fiber = yarn.Fiber;
						i.NeedleSize = yarn.NeedleSize;
						i.Length = yarn.Length;
						i.Tension = yarn.Tension;
						i.Washing = yarn.Washing;
						i.amount = yarn.amount;
						i.Color = yarn.Color;
						i.price = yarn.price;
						break;
					}
				}
				_dbYarnService.UpdateObjectAsync(yarn);
			}
		}
		public List<Wool> GetWools() { return _wools; }

		public List<Yarn> GetYarns() { return _yarns; }
	}
}
