using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Entities;

namespace OrderService.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext 
    {
        public ApplicationDbContext() : base()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB.; Database=OrderDB; Trusted_Connection=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Status).IsRequired();
                entity.Property(e => e.CreatedAt).IsRequired();
                entity.Property(e => e.UpdatedAt).IsRequired();
                entity.OwnsOne(e => e.Address, a =>
                {
                    a.Property(p => p.AddressLine).HasColumnName("AddressLine");
                    a.Property(p => p.City).HasColumnName("City");
                    a.Property(p => p.Country).HasColumnName("Country");
                    a.Property(p => p.CityCode).HasColumnName("CityCode").IsRequired();
                });

                entity.HasOne(e => e.Product)
                      .WithMany()
                      .HasForeignKey("ProductId")
                      .OnDelete(DeleteBehavior.Cascade);

            });

            modelBuilder.Entity<Product>(product =>
            {
                product.HasKey(p => p.Id);
                product.Property(p => p.ImageUrl).IsRequired();
                product.Property(p => p.Name).IsRequired();
            });

        }
    }
}
