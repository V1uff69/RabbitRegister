using Microsoft.EntityFrameworkCore;
using RabbitRegister.Model;

namespace RabbitRegister.EFDbContext
{
	public class ItemDbContext : DbContext
	{
		protected override void OnConfiguring(DbContextOptionsBuilder options)
		{
			options.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=RabbitRegister; Integrated Security=True; Connect Timeout=30; Encrypt=False");
		}
		public DbSet<Yarn> Yarns { get; set; }
		public DbSet<Product> Products { get; set; }
		
	}
}
