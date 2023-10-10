using Microsoft.EntityFrameworkCore;

namespace Dashboard.data;

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
        optionsBuilder.UseSqlite("Data Source=Dashboard.db");
        base.OnConfiguring(optionsBuilder);
    }
}