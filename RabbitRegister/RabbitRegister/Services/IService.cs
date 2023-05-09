using RabbitRegister.Model;

namespace RabbitRegister.Services
{
	public interface IService<T>
	{
		Task<IEnumerable<T>> GetObjectsAsync();
		Task AddObjectAsync(T obj);
		Task DeleteObjectAsync(T obj);
		Task UpdateObjectAsync(T obj);
		Task UpdateObjectAsync1(Product obj);
		Task<T> GetObjectByIdAsync(int id);
		Task SaveObjects(List<T> objs);
	}
}
