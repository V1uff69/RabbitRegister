using RabbitRegister.Model;

namespace RabbitRegister.Services.RabbitService
{
    public interface IRabbitService
    {
        Rabbit GetRabbit(int id, int breederRegNo);
        //Rabbit GetRabbit(int id);
        List<Rabbit> GetAllRabbits();
        Task AddRabbitAsync(RabbitDTO dto, Breeder breeder);
        //Task AddRabbitAsync(Rabbit rabbit, Breeder breeder);
        //Task UpdateRabbitAsync(Rabbit rabbit, int id);
        Task UpdateRabbitAsync(Rabbit rabbit, int id, int breederRegNo);
        Task<Rabbit> DeleteRabbitAsync(int? id, int? breederRegNo);
        IEnumerable<Rabbit> SearchByName(string str);
        IEnumerable<Rabbit> RatingFilter(int maxRating, int minRating = 0);
        IEnumerable<Rabbit> SortById();
        IEnumerable<Rabbit> SortByIdDescending();
        IEnumerable<Rabbit> SortByName();
        IEnumerable<Rabbit> SortByNameDescending();
        IEnumerable<Rabbit> SortByRating();
        IEnumerable<Rabbit> SortByRatingDescending();
        List<Rabbit> GetOwnedAliveRabbits(int breederRegNo);
        List<Rabbit> GetOwnedDeadRabbits(int breederRegNo);
        List<Rabbit> GetAllRabbitsWithConnectionsToMe(int breederRegNo);
        List<Rabbit> GetAllRabbitsWithOwner(int Owner);
        List<Rabbit> GetNotOwnedRabbitsWithMyBreederRegNo(int breederRegNo);
        List<Rabbit> GetIsForSaleRabbits();
    }
}
