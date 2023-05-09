using RabbitRegister.Model;

namespace RabbitRegister.Services.ProductService
{
    public interface IProductService
    {

        List<Wool> GetWools();
        List<Yarn> GetYarns();
        Task AddYarnAsync(Yarn yarn);
        void UpdateYarnAsync(Yarn yarn, int id);
        Yarn GetYarn(int yarnId);
        Task<Yarn> DeleteYarnAsync(int? Id);

    }
}
