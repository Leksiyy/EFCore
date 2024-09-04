using Microsoft.EntityFrameworkCore;

namespace homework1;

class Program
{
    static void Main(string[] args)
    {
        DbContextOptions<ApplicationContext> options = ApplicationOptionsSetter.SetOptions("DefaultConnection");

        //recreate db
        using (ApplicationContext db = new ApplicationContext(options))
        {
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
        }
        
        TrainActions.AddTrain(new Train
        {
            Color = 1,
            IsCoupe = false,
            Volume = 44,
            MaxSpeed = 120,
            Year = new DateTime(2020, 10, 10)
        }, options);
        TrainActions.AddTrain(new Train
        {
            Color = 7,
            IsCoupe = true,
            Volume = 36,
            MaxSpeed = 100,
            Year = new DateTime(2019, 10, 10)
        }, options);
        TrainActions.AddTrain(new Train
        {
            Color = 3,
            IsCoupe = false,
            Volume = 40,
            MaxSpeed = 90,
            Year = new DateTime(2019, 7, 4)
        }, options);

        List<Train> trains = TrainActions.ReciveTrains(options);

        TrainActions.EditTrain(1, new Train
        {
            Color = 2, //changed from 1 to 2
        }, options);
        
        trains = TrainActions.ReciveTrains(options);
        
        TrainActions.DeleteTrain(1, options);
        
        trains = TrainActions.ReciveTrains(options);
    }

    static class TrainActions
    {
        static public void AddTrain(Train train, DbContextOptions<ApplicationContext> options)
        {
            using (ApplicationContext db = new ApplicationContext(options))
            {
                db.Trains.Add(train);
                
                db.SaveChanges();
            }
        }

        static public List<Train> ReciveTrains(DbContextOptions<ApplicationContext> options)
        {
            using (ApplicationContext db = new ApplicationContext(options))
            {
                return db.Trains.ToList();
            }
        }

        static public void EditTrain(int id, Train editedTrain, DbContextOptions<ApplicationContext> options)
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

        static public void DeleteTrain(int id, DbContextOptions<ApplicationContext> options)
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
