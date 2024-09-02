using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace EFCore;

class Program
{
    static void Main(string[] args)
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            
            db.Users.Add(new User
            {
                age = 22,
                name = "Oleg",
            });
            
            db.SaveChanges();

            List<User> list = db.Users.ToList();
            Console.WriteLine(1);
        } 
    }
}

class ApplicationContext : DbContext
{
    // public ApplicationContext(DbContextOptions options) : base(options) { }
    // private readonly StreamWriter writer = new StreamWriter("log.txt", true); //для того что бы логировать в файл
    public DbSet<User> Users { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost;Database=NewDatabase;User Id=SA;Password=MisterLeha197618;TrustServerCertificate=True;");
        // RelationalEventId
        // CoreEventId
        // SqlServerEventId
        optionsBuilder.LogTo(Console.WriteLine, new []{RelationalEventId.CommandExecuted}); // конкретизация логироования
    }
}

class User
{
    public int Id { get; set; }
    public string? name { get; set; }
    public int age { get; set; }
}

public class MyLoggerProvider : ILoggerProvider
{
    public ILogger CreateLogger(string categoryName)
    {
        return new MyLogger();
    }

    public void Dispose()
    {
        
    }

    class MyLogger : ILogger
    {
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception,
            Func<TState, Exception, string> formatter)
        {
            Console.WriteLine(formatter(state, exception));
            File.AppendAllText("log.txt", formatter(state, exception));
        }
    }
}

// Add-Migration Initial - Initial это имя миграции, оно должно быть каждый раз уникальным
// миграции это инструмент с помощью которого можно править таблицы в базах данных без потери данных
// Update-Database - применяет миграцию
// Remove-Migration - отменяет последнюю сделанную мигирацию (для того если ее не хотят утверждать выше стоящие программисты)
// и вообще так то миграции это очень похожая тема на коммиты из гита, потому что точно так же можно откатиться с помощью
// Update-Database (имя миграции к которой нужно откатиться)
// Get-Migration - это комманда которая покажет таблицу миграций и покажет какие миграции выполнены, а какие - нет.