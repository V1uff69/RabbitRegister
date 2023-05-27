using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RabbitRegister.Model;

namespace RabbitRegister.EFDbContext
{
	public class ItemDbContext : DbContext
	{
       
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=RabbitRegisterDb; Integrated Security=True; Connect Timeout=30; Encrypt=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Rabbit>()
                .HasKey(r => new { r.RabbitRegNo, r.BreederRegNo });
            modelBuilder.Entity<OrderLine>()
                .HasKey(o => new { o.OrderLineId, o.OrderId });
        }

        public DbSet<Wool> Wools { get; set; }
        public DbSet<Breeder> Breeders { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Trimming> Trimmings { get; set; }
		public DbSet<Yarn> Yarns { get; set; }
		public DbSet<Rabbit> Rabbits { get; set; }
		public DbSet<OrderLine> OrderLines { get; set; }
    }
}
