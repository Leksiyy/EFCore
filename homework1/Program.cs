using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace homework1;

class Program
{
    static void Main(string[] args)
    {
        var builder = new ConfigurationBuilder();
        builder.SetBasePath(Directory.GetCurrentDirectory());
        builder.AddJsonFile("appsettings.json");
        
        var config = builder.Build();
        string connectionString = config.GetConnectionString("DefaultConnection");

        var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
        var options = optionsBuilder.UseSqlServer(connectionString).Options;

        new Train
        {
            Color = 1,
            IsCoupe = false,
            Volume = 44,
            MaxSpeed = 120,
            Year = new DateTime(2020, 10, 10)
        };
    }

    class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options) {}
        public DbSet<Train> Trains { get; set; }
    }

    class TrainActions
    {
        public void AddTrain(Train train, DbContextOptions<ApplicationContext> options)
        {
            using (ApplicationContext db = new ApplicationContext(options))
            {
                db.Trains.Add(train);
                
                db.SaveChanges();
            }
        }

        public List<Train> ReciveTrains(DbContextOptions<ApplicationContext> options)
        {
            using (ApplicationContext db = new ApplicationContext(options))
            {
                return db.Trains.ToList();
            }
        }

        public void EditTrain(int id, Train editedTrain, DbContextOptions<ApplicationContext> options)
        {
            using (ApplicationContext db = new ApplicationContext(options))
            {
                var train = db.Trains.FirstOrDefault(train => train.Id == id);
                if (train != null)
                {
                    train.Color = editedTrain.Color;
                    train.MaxSpeed = editedTrain.MaxSpeed;
                    train.IsCoupe = editedTrain.IsCoupe;
                    train.Volume = editedTrain.Volume;
                    train.Year = editedTrain.Year;

                    db.SaveChanges();
                }
            }
        }

        public void DeleteTrain(int id, DbContextOptions<ApplicationContext> options)
        {
            using (ApplicationContext db = new ApplicationContext(options))
            {
                var train = db.Trains.FirstOrDefault(train => train.Id == id);
                if (train != null)
                {
                    db.Trains.Remove(train);

                    db.SaveChanges();
                }
            }
        }
    }
    
}
