using Microsoft.EntityFrameworkCore;
using RabbitRegister.Model;

namespace RabbitRegister.EFDbContext
{
	public class ItemDbContext : DbContext
	{
		protected override void OnConfiguring(DbContextOptionsBuilder options)
		{
<<<<<<<<< Temporary merge branch 1
			options.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=RabbitRegister; Integrated Security=True; Connect Timeout=30; Encrypt=False");
		}
		public DbSet<Yarn> Yarns { get; set; }
		public DbSet<Product> Products { get; set; }
		
	}
=========
			options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=RabbitRegisterDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
		}

		public DbSet<Wool> Wools { get; set; }
		public DbSet<Breeder> Breeders { get; set; }
		public DbSet<Order> Orders { get; set; }
        public DbSet<Trimming> Trimmings { get; set; }
    }
>>>>>>>>> Temporary merge branch 2
}
