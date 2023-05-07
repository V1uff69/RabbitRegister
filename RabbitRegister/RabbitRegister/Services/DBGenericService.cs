using Microsoft.EntityFrameworkCore;
using RabbitRegister.EFDbContext;

namespace RabbitRegister.Services
{
    public class DbGenericService<T> : IService<T> where T : class
    {
		
        public async Task<IEnumerable<T>> GetObjectsAsync()
        {
            using (var context = new ItemDbContext())
            {
                return await context.Set<T>().AsNoTracking().ToListAsync();
            }
        }
		/// <summary>
		/// BEMÆRK
		/// Der benyttes await og SaveChangesAsync for at sikre asynkron afvikling
		/// og at der ikke returneres fra metoden før opdateringen er gennemført.
		/// </summary>

        public async Task AddObjectAsync(T obj)
        {
            using (var context = new ItemDbContext())
            {
                context.Set<T>().Add(obj);
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteObjectAsync(T obj)
        {
            using (var context = new ItemDbContext())
            {
                context.Set<T>().Remove(obj);
                await context.SaveChangesAsync();
            }

        }

        public async Task UpdateObjectAsync(T obj)
		public async Task SaveObjects(List<T> objs)
        {
            using (var context = new ItemDbContext())
            {
                context.Set<T>().Update(obj);
                await context.SaveChangesAsync();
				foreach (T obj in objs)
				{
					context.Set<T>().Add(obj);
					context.SaveChanges();
				}

				context.SaveChanges();
            }
        }

        public async Task<T> GetObjectByIdAsync(int id)
        {
            using (var context = new ItemDbContext())
            {
                return await context.Set<T>().FindAsync(id);
            }
        }

        public async Task SaveObjects(List<T> objs)

		public async Task UpdateObjectAsync(T obj)
        {
            using (var context = new ItemDbContext())
            {
                foreach (T obj in objs)
                {
                    context.Set<T>().Add(obj);
                    context.SaveChanges();
                }

                context.SaveChanges();
				context.Set<T>().Update(obj);
				await context.SaveChangesAsync();
            }
        }
    }
}
