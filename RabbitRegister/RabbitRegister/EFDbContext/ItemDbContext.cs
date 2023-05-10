using Microsoft.EntityFrameworkCore;
using RabbitRegister.Model;

namespace RabbitRegister.EFDbContext
{
	public class ItemDbContext : DbContext
	{
		protected override void OnConfiguring(DbContextOptionsBuilder options)
		{
			options.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=RabbitReg_DB; Integrated Security=True; Connect Timeout=30; Encrypt=False");
			//options.UseSqlServer();
		}

		public DbSet<Breeder> Breeders { get; set; }
		public DbSet<Rabbit> Rabbits { get; set; }
		public DbSet<Trimming> Trimmings { get; set; }
		//public DbSet<Wool> Wools { get; set; }
		//public DbSet<Order> Orders { get; set; }
	}

}
