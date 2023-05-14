using RabbitRegister.Model;

namespace RabbitRegister.Services.TrimmingService
{
	public interface ITrimmingService
	{
        Task AddTrimmingAsync(Trimming trimming);
		void UpdateTrimming(Trimming trimming, int id);
		Trimming GetTrimming(int id);
		Trimming DeleteTrimming(int? id);
        IEnumerable<Trimming> NameSearch(string str);
        IEnumerable<Trimming> RabbitIdSearch(int id);
        IEnumerable<Trimming> SortById();
        IEnumerable<Trimming> SortByIdDescending();
        IEnumerable<Trimming> SortByRabbitId();
        IEnumerable<Trimming> SortByRabbitIdDescending();
        IEnumerable<Trimming> SortByDate();
        IEnumerable<Trimming> SortByDateDescending();
        List<Trimming> GetTrimmings();
    }
}
