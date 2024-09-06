using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EFCore;


public class ApplicationContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Languages> LanguagesEnumerable { get; set; }
 
    private IConfigurationRoot config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json").Build();
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
    }
 
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Language>().HasOne(e => e.Languages).WithOne(e => e.Language).HasForeignKey(e = > e.?????); ///////
        
        modelBuilder.Entity<User>(e => e.ComplexProperty(
            e => e.Language,
            b =>
            {
                b.ComplexProperty(e => e.LanguageDetails);
            }
        ));
        base.OnModelCreating(modelBuilder);
    }
}

