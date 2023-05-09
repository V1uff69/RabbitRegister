using Microsoft.EntityFrameworkCore;
using RabbitRegister.Model;

namespace RabbitRegister.EFDbContext
{
	public class ItemDbContext : DbContext
	{
		protected override void OnConfiguring(DbContextOptionsBuilder options)
		{
			options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=RabbitRegisterDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
		}

		public DbSet<Wool> Wools { get; set; }
		public DbSet<Breeder> Breeders { get; set; }
		public DbSet<Order> Orders { get; set; }
	}
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=RabbitRegister; Integrated Security=True; Connect Timeout=30; Encrypt=False");
        }
        public DbSet<Trimming> Trimmings { get; set; }
    }
}
