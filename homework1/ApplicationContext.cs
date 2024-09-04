using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace homework1;

static class ApplicationOptionsSetter
{
    public static DbContextOptions<ApplicationContext> SetOptions(string ConnectionString)
    {
        var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");

        string connectionString = builder.Build().GetConnectionString(ConnectionString);

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