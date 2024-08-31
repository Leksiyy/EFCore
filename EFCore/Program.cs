using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EFCore;

class Program
{
    static void Main(string[] args)
    {
        // using (ApplicationContext db = new ApplicationContext())
        // {
        // User user = new User
        // {
        //     Name = "Alex",
        //     Age = 19,
        // };
        // User user1 = new User
        // {
        //     Name = "Misha",
        //     Age = 44,
        // };
        // User user2 = new User
        // {
        //     Name = "Olga",
        //     Age = 24,
        // };
        //
        // db.Users.Add(user);
        // db.Users.Add(user1);
        // db.Users.Add(user2);
        // db.SaveChanges();

        // List<User> users = db.Users.ToList();
        // }

        var builder = new ConfigurationBuilder();
        builder.SetBasePath(Directory.GetCurrentDirectory());
        builder.AddJsonFile("appsettings.json");
        var config = builder.Build();
        // string connectionString = config.GetConnectionString("DefaultConnection");
        string connectionString = config.GetSection("ConnectionStrings:DefaultConnection").Value;
            
        var optionsBulider = new DbContextOptionsBuilder<ApplicationContext>();
        var options = optionsBulider.UseSqlServer(connectionString).Options;

        using (ApplicationContext db = new ApplicationContext(options))
        {
            var AllUsers = db.Users.ToList();
        }
    }

    class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; } //представление таблицы

        // public ApplicationContext()
        // {
        //     Database.EnsureCreated();
        // }

        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // { 
        //     //dotnet ef dbcontext scafold "Server=localhost;Database=NewDatabase;User Id=SA;Password=MisterLeha197618;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer
        //     //string str = "Server=localhost;Database=master;Trusted_Connection=True;TrustServerCertificate=True;";
        //     string str = "Server=localhost;Database=NewDatabase;User Id=SA;Password=MisterLeha197618;TrustServerCertificate=True;";
        //     optionsBuilder.UseSqlServer(str);
        // }
    }
}