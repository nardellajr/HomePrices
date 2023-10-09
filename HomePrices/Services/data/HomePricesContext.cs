using Microsoft.EntityFrameworkCore;

namespace HomePrices.data;

public class HomePricesContext : DbContext
{
    public HomePricesContext()
    {        
    }

    public HomePricesContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Home> Homes => Set<Home>();
    public DbSet<Region> Regions => Set<Region>();    

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=HomePrices.db");
        base.OnConfiguring(optionsBuilder);
    }
}