using AutoRepair.Model;
using Microsoft.EntityFrameworkCore;

namespace AutoRepair
{
    public sealed class AppContext : DbContext
    {
        public AppContext()
        {
            Database.EnsureCreated();

        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<CarModel> CarModels { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Spare> Spares { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=AutoRepair;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           /* modelBuilder.Entity<OrderServices>().HasKey(os => new {os.ServiceId, os.OrderId});
            modelBuilder.Entity<OrderServices>().HasOne<Order>(os => os.Order).WithMany(o => o.OrderServices)
                .HasForeignKey(os => os.OrderId);
            modelBuilder.Entity<OrderServices>().HasOne<Service>(os => os.Service).WithMany(s => s.OrderServices)
                .HasForeignKey(os => os.ServiceId);
            modelBuilder.Entity<Car>().HasOne(p => p.CarOwner).WithMany(p => p.ClientCars).OnDelete(DeleteBehavior.Cascade);
            */
        }
    }
}