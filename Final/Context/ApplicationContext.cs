using Final.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EFCore;


public class ApplicationContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Settings> Settings { get; set; }
    
    private IConfigurationRoot config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json").Build();
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
    }
 
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasOne(e => e.Settings).WithOne(e => e.User);
        modelBuilder.Entity<User>().HasMany(e => e.Transactions).WithOne(e => e.User).HasForeignKey(t => t.UserId);
        modelBuilder.Entity<Transaction>().HasMany(e => e.Category).WithOne(e => e.Transaction);
        
        base.OnModelCreating(modelBuilder);
    }
}

