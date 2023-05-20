using RabbitRegister.Model;

namespace RabbitRegister.Services.RabbitService
{
    public interface IRabbitService
    {
        List<Rabbit> GetRabbits();
        Task AddRabbitAsync(Rabbit rabbit);
        Task UpdateRabbitAsync(Rabbit rabbit, int id);
        Rabbit GetRabbit(int id);
        Task<Rabbit> DeleteRabbitAsync(int? rabbitId);
        IEnumerable<Rabbit> NameSearch(string str);
        IEnumerable<Rabbit> RatingFilter(int maxRating, int minRating = 0);
        IEnumerable<Rabbit> SortById();
        IEnumerable<Rabbit> SortByIdDescending();
        IEnumerable<Rabbit> SortByName();
        IEnumerable<Rabbit> SortByNameDescending();
        IEnumerable<Rabbit> SortByRating();
        IEnumerable<Rabbit> SortByRatingDescending();
        List<Rabbit> GetOwnedAliveRabbits(int breederRegNo);
        List<Rabbit> GetOwnedDeadRabbits(int breederRegNo);
        List<Rabbit> GetAllOwnedRabbits(int breederRegNo);
        List<Rabbit> GetAllRabbitsWithConnectionsToMe(int breederRegNo);
        List<Rabbit> GetNotOwnedRabbitsWithMyBreederRegNo(int breederRegNo);
    }
}
