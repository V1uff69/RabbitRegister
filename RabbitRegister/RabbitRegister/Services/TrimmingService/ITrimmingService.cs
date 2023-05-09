using RabbitRegister.Model;

namespace RabbitRegister.Services.TrimmingService
{
	public interface ITrimmingService
	{
		Task AddTrimmingAsync(Trimming trimming);
		void UpdateTrimming(Trimming trimming, int id);
		Trimming GetTrimming(int id);
		Trimming DeleteTrimming(int? id);
		List<Trimming> GetTrimmings();
    }
}
