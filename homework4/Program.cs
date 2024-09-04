using Microsoft.EntityFrameworkCore;

namespace homework4;

class Program
{
    static void Main(string[] args)
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            
            db.Users.AddRange(new User
            {
                Name = "Alan",
                UserSettings = new UserSettings
                {
                    GuiScale = 3
                }
            },new User
            {
                Name = "Mike",
                UserSettings = new UserSettings
                {
                    GuiScale = 5
                }
            },new User
            {
                Name = "Olaf",
                UserSettings = new UserSettings
                {
                    GuiScale = 1
                }
            });

            db.SaveChanges();

            var selectedUser = db.Users.FirstOrDefault(e => e.Id == 2);
            if (selectedUser != null)
            {
                db.Users.Remove(selectedUser);
                db.SaveChanges();
            }

            List<User> list = db.Users.Include(e => e.UserSettings).ToList();
            List<UserSettings> list2 = db.UserSettings.Include(e => e.User).ToList();
        }
    }
}