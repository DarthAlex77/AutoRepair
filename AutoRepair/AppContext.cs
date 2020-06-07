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

        public DbSet<Car>      Cars      { get; set; }
        public DbSet<CarModel> CarModels { get; set; }
        public DbSet<Client>   Clients   { get; set; }
        public DbSet<Order>    Orders    { get; set; }
        public DbSet<Service>  Services  { get; set; }
        public DbSet<Spare>    Spares    { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=AutoRepair;Trusted_Connection=True;");
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
             modelBuilder.Entity<OrdersServices>().HasKey(os => new {os.ServiceId, os.OrderId});
             modelBuilder.Entity<OrdersServices>().HasOne<Order>(os => os.Order).WithMany(o => o.OrderServices)
                 .HasForeignKey(os => os.OrderId);
             modelBuilder.Entity<OrdersServices>().HasOne<Service>(os => os.Service).WithMany(s => s.OrderServices)
                 .HasForeignKey(os => os.ServiceId);
            
            modelBuilder.Entity<Car>().HasOne(p => p.CarOwner).WithMany(p => p.ClientCars).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrdersSpares>().HasKey(os => new {os.SpareId, os.OrderId});
            modelBuilder.Entity<OrdersSpares>().HasOne<Order>(os => os.Order).WithMany(o => o.OrdersSpares)
                .HasForeignKey(os => os.OrderId);
            modelBuilder.Entity<OrdersSpares>().HasOne<Spare>(os => os.Spare).WithMany(s => s.OrdersSpares)
                .HasForeignKey(os => os.SpareId);
        }
    }
}