using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace homework2;

class Program
{
    static void Main(string[] args)
    {
        // консольные комманды которые использовались при выполнении домашнего задания:
        // 
        // dotnet ef migrations add initial
        // dotnet ef database update
        // dotnet ef migrations add orders
        // dotnet ef database update
        // dotnet ef migrations add linking
        // dotnet ef database update
        

        // using (ApplicationContext db = new ApplicationContext())
        // {
        //     
        // }
        
        using (ApplicationContext db = new ApplicationContext())
        {
            db.Products.AddRange(new Product
            {
                Category = "eat",
                Price = 20,
                Weight = 1,
            }, new Product
            {
                Category = "dress",
                Price = 1200,
                Weight = 3,
            }, new Product
            {
                Category = "subscription",
                Price = 34,
                Weight = null,
            });
            
            db.SaveChanges();
        }
    }
}

class ApplicationContext : DbContext
{
    private IConfigurationRoot config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json").Build(); 
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
        
        base.OnConfiguring(optionsBuilder);
    }
    
    public DbSet<Product> Products { get; set; }
    
    // добавлено после написания второй комманды
    public DbSet<Order> Orders { get; set; }
}

