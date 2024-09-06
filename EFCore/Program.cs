using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace EFCore;

class Program
{
    static void Main()
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            var l1 = new Languages
            {
                Name = "English",
            };
            var l2 = new Languages
            {
                Name = "Spanish",
            };
            
            db.LanguagesEnumerable.AddRange(l1, l2);
            
            List<User> users = new List<User>()
            {
                new User
                {
                    Name = "Alex",
                    Language = new Language
                    {
                         Languages = l1,
                         
                         LanguageDetails = new LanguageDetails
                         {
                            LanguageLevel = LanguageLevel.A2,
                            TrainingStartDate = new DateOnly(2023,05,14)
                         }
                     }
                },
                new User
                {
                    Name = "Marry",
                    Language = new Language
                    {
                         Languages = l2,
                         LanguageDetails = new LanguageDetails
                         {
                            LanguageLevel = LanguageLevel.C1,
                            TrainingStartDate = new DateOnly(2020,01,10)
                         }
                     }
                }
            };
 
            db.Users.AddRange(users);
            db.SaveChanges();
        }
    }
}
 
