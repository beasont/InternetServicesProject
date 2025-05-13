using ComputerStore.Domain;
using Microsoft.EntityFrameworkCore;

namespace ComputerStore.Infrastructure;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Product>(e =>
        {
            e.Property(p => p.Price).HasPrecision(18, 2);
        });

        base.OnModelCreating(builder);
    }
}
