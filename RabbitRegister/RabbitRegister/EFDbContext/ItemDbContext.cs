using Microsoft.EntityFrameworkCore;
using RabbitRegister.Model;

namespace RabbitRegister.EFDbContext
{
	public class ItemDbContext : DbContext
	{
		protected override void OnConfiguring(DbContextOptionsBuilder options)
		{
			//options.UseSqlServer();
		}

		public DbSet<Wool> Wools { get; set; }
		public DbSet<Breeder> Breeders { get; set; }
		public DbSet<Order> Orders { get; set; }
	}
}
