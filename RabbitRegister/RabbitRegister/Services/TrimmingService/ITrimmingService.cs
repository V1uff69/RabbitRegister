using RabbitRegister.Model;

namespace RabbitRegister.Services.TrimmingService
{
	public interface ITrimmingService
	{
		Task AddTrimmingAsync(Trimming trimming);
		Trimming GetTrimming(int id);
		Trimming DeleteTrimming(int? id);
		List<Trimming> GetTrimmings();
    }
}
