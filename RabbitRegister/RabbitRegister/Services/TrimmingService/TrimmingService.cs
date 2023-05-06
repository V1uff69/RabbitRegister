using Microsoft.EntityFrameworkCore;
using RabbitRegister.EFDbContext;
using RabbitRegister.Model;

namespace RabbitRegister.Services.TrimmingService
{
    public class TrimmingService : ITrimmingService
    {
        private List<Trimming> _trimmings;

		private DbGenericService<Trimming> DbGenericService;

		public TrimmingService(DbGenericService<Trimming> dbGenericService)
		{
			DbGenericService = dbGenericService;
			_trimmings = dbGenericService.GetObjectsAsync().Result.ToList();
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
