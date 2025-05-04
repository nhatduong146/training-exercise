using Microsoft.EntityFrameworkCore;
using RESTfullAPI.Domain.Entities;

namespace RESTfullAPI.Infrastructure.DbContexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
            .Property(p => p.RowVersion)
            .IsRowVersion();

            base.OnModelCreating(modelBuilder);
        }
    }
}
