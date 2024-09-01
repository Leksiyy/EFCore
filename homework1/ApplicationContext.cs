using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;

namespace homework1;

static class ApplicationOptionsSetter
{
    public static DbContextOptions<ApplicationContext> SetOptions(string ConnectionString)
    {
        var builder = new ConfigurationBuilder();
        builder.SetBasePath(Directory.GetCurrentDirectory());
        builder.AddJsonFile("appsettings.json");

        var config = builder.Build();
        string connectionString = config.GetConnectionString(ConnectionString);

        var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
        var options = optionsBuilder.UseSqlServer(connectionString).Options;

        return options;
    }
}

class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions options) : base(options) {}
    public DbSet<Train> Trains { get; set; }
}