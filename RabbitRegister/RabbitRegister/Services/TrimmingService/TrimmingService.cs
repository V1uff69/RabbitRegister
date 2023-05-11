using Microsoft.EntityFrameworkCore;
using RabbitRegister.EFDbContext;
using RabbitRegister.Model;
using System.Data.SqlTypes;

namespace RabbitRegister.Services.TrimmingService
{
    public class TrimmingService : ITrimmingService
    {
        private List<Trimming> _trimmings;

		private DbGenericService<Trimming> DbGenericService;

		public TrimmingService(DbGenericService<Trimming> dbGenericService)
		{
			DbGenericService = dbGenericService;
			_trimmings = DbGenericService.GetObjectsAsync().Result.ToList();
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
        public List<Trimming> GetTrimmings() { return _trimmings; }
    }
}
