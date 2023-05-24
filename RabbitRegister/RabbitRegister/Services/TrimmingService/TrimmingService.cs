using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;
using RabbitRegister.EFDbContext;
using RabbitRegister.MockData;
using RabbitRegister.Model;
using RabbitRegister.Services.RabbitService;
using System.Data.SqlTypes;

namespace RabbitRegister.Services.TrimmingService
{
    public class TrimmingService : ITrimmingService
    {
        private List<Trimming> _trimmings;

		private DbGenericService<Trimming> DbGenericService;
		private IRabbitService RabbitService; 

		public TrimmingService(DbGenericService<Trimming> dbGenericService, IRabbitService rabbitService)
		{
			DbGenericService = dbGenericService;
			RabbitService = rabbitService;
			_trimmings = DbGenericService.GetObjectsAsync().Result.ToList();

			//_trimmings = MockTrimming.GetMockTrimming(); //DB tom? Ved første Debug kør denne kode, og udkommenter igen derefter
			//foreach (var trimming in _trimmings)
			//{
			//	DbGenericService.AddObjectAsync(trimming).Wait();
			//}
		}
        public Trimming GetTrimming(int id)
		{
			foreach (Trimming trimming in _trimmings)
			{
				if (trimming.TrimmingId == id)
					return trimming;
			}
			return null;
		}
		public async Task AddTrimmingAsync (Trimming trimming)
		{
            await DbGenericService.AddObjectAsync(trimming);
            _trimmings.Add(trimming);
		}
		
		public void UpdateTrimming(Trimming trimming, int id)
		{
			if (trimming != null)
			{
				foreach (Trimming trim in _trimmings)
				{
					if (trim.TrimmingId == id)
					{
						trim.RabbitRegNo = trimming.RabbitRegNo;
						trim.BreederRegNo = trimming.BreederRegNo;
						trim.Name = trimming.Name;
						trim.Date = trimming.Date;
						trim.TimeUsed = trimming.TimeUsed;
						trim.HairLengthByDayNinety = trimming.HairLengthByDayNinety;
						trim.WoolDensity = trimming.WoolDensity;
						trim.FirstSortmentWeight = trimming.FirstSortmentWeight;
						trim.SecondSortmentWeight = trimming.SecondSortmentWeight;
						trim.DisposableWoolWeight = trimming.DisposableWoolWeight;
						break;
					}
				}
				DbGenericService.UpdateObjectAsync(trimming);
			}
		}

		public Trimming DeleteTrimming(int? trimmingId)
		{
			Trimming trimmingToBeDeleted = null;
			foreach (Trimming trimming in _trimmings)
			{
				if (trimming.TrimmingId == trimmingId)
				{
					trimmingToBeDeleted = trimming;
					break;
				}
			}

			if (trimmingToBeDeleted != null)
			{
				_trimmings.Remove(trimmingToBeDeleted);
				DbGenericService.DeleteObjectAsync(trimmingToBeDeleted);
			}
			return trimmingToBeDeleted;
		}

        public IEnumerable<Trimming> SortById(int Owner)
        {
            return GetTrimmingsByOwnerId(Owner).OrderBy(r => r.TrimmingId);
        }

        public IEnumerable<Trimming> SortByIdDescending(int Owner)
        {
            return GetTrimmingsByOwnerId(Owner).OrderByDescending(r => r.TrimmingId);
        }

        public IEnumerable<Trimming> SortByRabbitId()
        {
            return _trimmings.OrderBy(r => r.RabbitRegNo);
        }

        public IEnumerable<Trimming> SortByRabbitIdDescending()
        {
            return _trimmings.OrderByDescending(r => r.RabbitRegNo);
        }

        public IEnumerable<Trimming> SortByDate(int Owner)
        {
            return GetTrimmingsByOwnerId(Owner).OrderBy(r => r.Date);
        }

        public IEnumerable<Trimming> SortByDateDescending(int Owner)
        {
            return GetTrimmingsByOwnerId(Owner).OrderByDescending(r => r.Date);
        }

        public IEnumerable<Trimming> NameSearch(string str, int Owner)
        {
            if (string.IsNullOrEmpty(str))
            {
                return GetTrimmingsByOwnerId(Owner);
            }
            else
            {
                return GetTrimmingsByOwnerId(Owner).Where(Trimming => Trimming.Name.ToLower().Contains(str.ToLower()));
            }
        }

        public IEnumerable<Trimming> RabbitIdSearch(int id)
        {
            if (id == 0)
            {
                return _trimmings;
            }
            else
            {
                return _trimmings.Where(Trimming => Trimming.RabbitRegNo == id);
            }
        }

		public List<Trimming> GetTrimmingByRabbitRegNoAndBreederRegNo (int RabbitRegNo, int BreederRegNo)
		{
			var Trimmings = GetTrimmings();
			List<Trimming> TrimmingByRabbitRegNo = new List<Trimming>();
			foreach (var Trimming in Trimmings)
			{
				if (Trimming.RabbitRegNo == RabbitRegNo && Trimming.BreederRegNo == BreederRegNo)
					TrimmingByRabbitRegNo.Add(Trimming);
			}
			return TrimmingByRabbitRegNo;
		}

		public List<Trimming> GetTrimmingsByOwnerId(int Owner)
		{
			List<Rabbit> rabbitsByOwner = RabbitService.GetAllRabbitsWithOwner(Owner);

			List<Trimming> trimmingsByOwner = new List<Trimming>();
			foreach (var Rabbit in rabbitsByOwner)
			{
				var trimmingsForRabbit = _trimmings.Where(trimming => trimming.BreederRegNo == Rabbit.BreederRegNo && trimming.RabbitRegNo == Rabbit.RabbitRegNo).ToList();

				trimmingsByOwner.AddRange(trimmingsForRabbit);
			}
			return trimmingsByOwner;
		}

        public List<Trimming> GetTrimmings() { return _trimmings; }
    }
}
