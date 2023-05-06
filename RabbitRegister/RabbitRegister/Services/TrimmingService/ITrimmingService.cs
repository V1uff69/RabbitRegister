using RabbitRegister.Model;

namespace RabbitRegister.Services.TrimmingService
{
	public interface ITrimmingService
	{
		void AddTrimming(Trimming trimming);
		Trimming GetTrimming(int id);
		List<Trimming> GetTrimmings();
	}
}
