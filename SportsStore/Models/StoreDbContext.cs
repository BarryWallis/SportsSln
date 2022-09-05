using Microsoft.EntityFrameworkCore;

namespace SportsStore.Models;

public class StoreDbContext : DbContext
{
    public StoreDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Product> Products => Set<Product>();
}
