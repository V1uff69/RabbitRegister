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
		public void AddTrimming (Trimming trimming)
		{
			_trimmings.Add(trimming);
			DbGenericService.AddObjectAsync(trimming);
		}
		public List<Trimming> GetTrimmings() { return _trimmings; }
	}
}
