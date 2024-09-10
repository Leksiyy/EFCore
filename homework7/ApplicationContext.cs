using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace homework7;

public class ApplicationContext : DbContext
{
    public DbSet<Client> Clients { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    
    private IConfigurationRoot _config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json").Build();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>()
            .HasMany(e => e.Orders)
            .WithOne(e => e.Client)
            .HasForeignKey(e => e.ClientId);
        
        modelBuilder.Entity<Order>()
            .HasMany(e => e.OrderDetail)
            .WithOne(e => e.Order)
            .HasForeignKey(e => e.OrderId);

        modelBuilder.Entity<Product>()
            .HasMany(e => e.OrderDetail)
            .WithOne(e => e.Product)
            .HasForeignKey(e => e.ProductId);
        
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_config.GetConnectionString("DefaultConnection"));
        base.OnConfiguring(optionsBuilder);
    }
}