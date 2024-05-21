
using CustomerService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomerService.Infrastructure.Data
{
    public class CustomerDbContext : DbContext
    {
        public CustomerDbContext() :base()
        {
        }

        public CustomerDbContext(DbContextOptions<CustomerDbContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB.; Database=CustomerDB; Trusted_Connection=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Email).IsRequired();
                entity.OwnsOne(e => e.Address, a =>
                {   
                    a.Property(p => p.AddressLine).HasColumnName("AddressLine");
                    a.Property(p => p.City).HasColumnName("City");
                    a.Property(p => p.Country).HasColumnName("Country");
                    a.Property(p => p.CityCode).HasColumnName("CityCode").IsRequired();
                });
                entity.Property(e => e.CreatedAt).IsRequired();
                entity.Property(e => e.UpdatedAt).IsRequired();
            });
        }
    }
}
