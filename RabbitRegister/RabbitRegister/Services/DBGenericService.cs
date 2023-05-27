using Microsoft.EntityFrameworkCore;
using RabbitRegister.EFDbContext;

namespace RabbitRegister.Services
{
    public class DbGenericService<T> : IService<T> where T : class
    {
        /// <summary>
        /// Gets readonly objects/entities as a list from database
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> GetObjectsAsync()
        {
            using (var context = new ItemDbContext())
            {
                return await context.Set<T>().AsNoTracking().ToListAsync();
            }
        }
        /// <summary>
        /// Saves object/entity to database
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual async Task AddObjectAsync(T obj)
        {
            using (var context = new ItemDbContext())
            {
                context.Set<T>().Add(obj);
                await context.SaveChangesAsync();
            }
        }
        /// <summary>
        /// Deletes object/entity from database
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task DeleteObjectAsync(T obj)
        {
            using (var context = new ItemDbContext())
            {
                context.Set<T>().Remove(obj);
                await context.SaveChangesAsync();
            }
        }
        /// <summary>
        /// Saves a list of objects/entities to database
        /// </summary>
        /// <param name="objs"></param>
        /// <returns></returns>
        public async Task SaveObjects(List<T> objs)
        {
            using (var context = new ItemDbContext())
            {
                foreach (T obj in objs)
                {
                    context.Set<T>().Add(obj);
                    context.SaveChanges();
                }

                context.SaveChanges();
            }
        }
        /// <summary>
        /// Gets a specific database object/entity by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> GetObjectByIdAsync(int id)
        {
            using (var context = new ItemDbContext())
            {
                return await context.Set<T>().FindAsync(id);
            }
        }
        /// <summary>
        /// Updates an object/entity in the database
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task UpdateObjectAsync(T obj)
        {
            using (var context = new ItemDbContext())
            {
                context.Set<T>().Update(obj);
                await context.SaveChangesAsync();
            }
        }
    }
}
