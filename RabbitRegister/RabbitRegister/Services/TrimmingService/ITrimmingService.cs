using RabbitRegister.Model;

namespace RabbitRegister.Services.TrimmingService
{
	public interface ITrimmingService
	{
        Task AddTrimmingAsync(Trimming trimming);
		void UpdateTrimming(Trimming trimming, int id);
		Trimming GetTrimming(int id);
		Trimming DeleteTrimming(int? id);
        IEnumerable<Trimming> NameSearch(string str, int Owner);
        IEnumerable<Trimming> RabbitIdSearch(int id);
        IEnumerable<Trimming> SortById(int Owner);
        IEnumerable<Trimming> SortByIdDescending(int Owner);
        IEnumerable<Trimming> SortByRabbitId();
        IEnumerable<Trimming> SortByRabbitIdDescending();
        IEnumerable<Trimming> SortByDate(int Owner);
        IEnumerable<Trimming> SortByDateDescending(int Owner);
        List<Trimming> GetTrimmingsByOwnerId(int Owner);
        List<Trimming> GetTrimmingByRabbitRegNoAndBreederRegNo(int RabbitRegNo, int BreederRegNo);
        List<Trimming> GetTrimmings();
    }
}
