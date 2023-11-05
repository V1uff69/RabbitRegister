using RabbitRegister.Model;

namespace RabbitRegister.Services.RabbitService
{
    public interface IRabbitService
    {
        Rabbit GetRabbit(int id, int originRegNo);
        //Rabbit GetRabbit(int id);
        List<Rabbit> GetAllRabbits();
        Task AddRabbitAsync(RabbitDTO dto, Breeder breeder);
        //Task AddRabbitAsync(Rabbit rabbit, Breeder breeder);
        //Task UpdateRabbitAsync(Rabbit rabbit, int id);
        Task UpdateRabbitAsync(Rabbit rabbit, int id, int originRegNo);
        Task<Rabbit> DeleteRabbitAsync(int? id, int? originRegNo);
        IEnumerable<Rabbit> SearchByName(string str);
        IEnumerable<Rabbit> RatingFilter(int? maxRating, int? minRating);
        IEnumerable<Rabbit> SortById();
        IEnumerable<Rabbit> SortByIdDescending();
        IEnumerable<Rabbit> SortByName();
        IEnumerable<Rabbit> SortByNameDescending();
        IEnumerable<Rabbit> SortByRating();
        IEnumerable<Rabbit> SortByRatingDescending();
        List<Rabbit> GetOwnedAliveRabbits(int originRegNo);
        List<Rabbit> GetOwnedDeadRabbits(int originRegNo);
        List<Rabbit> GetAllRabbitsWithConnectionsToMe(int originRegNo);
        List<Rabbit> GetAllRabbitsWithOwner(int Owner);
        List<Rabbit> GetNotOwnedRabbitsWithMyBreederRegNo(int originRegNo);
        List<Rabbit> GetIsForSaleRabbits();
    }
}
