using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace homework3;

public class ApplicationContext : DbContext
{
    private IConfigurationRoot config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json").Build();

    public DbSet<User> Users { get; set; }

    public static void RecreateDB()
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
        }
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasIndex(u => u.Login).IsUnique();
        base.OnModelCreating(modelBuilder);
    }
}