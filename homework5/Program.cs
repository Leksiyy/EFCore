using Microsoft.EntityFrameworkCore;

namespace homework5;

class Program
{
    static void Main(string[] args)
    {
        using (ApplicaitonContext db = new ApplicaitonContext())
        {
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            
            db.Events.AddRange(new Event
            {
                isPrivate = false,
                Location = "Paris",
                Name = "Olimpic games"
            },new Event
            {
                isPrivate = true,
                Location = "night club \"DENIS\"",
                Name = "dj party"
            },new Event
            {
                isPrivate = false,
                Location = "odessa beach",
                Name = "concert"
            });
            
            db.Guests.AddRange(new Guest
            {
                isVip = false,
                Name = "Styopa"
            },new Guest
            {
                isVip = false,
                Name = "Anya"
            },new Guest
            {
                isVip = true,
                Name = "Vasiliy"
            });
            db.SaveChanges();

            var Styopa = db.Guests.FirstOrDefault(e => e.Name == "Styopa");
            var odessa_beach = db.Events.FirstOrDefault(e => e.Name == "odessa beach");

            if (Styopa != null && odessa_beach != null)
            {
                db.EventGuests.Add(new EventGuest
                {
                    GuestId = Styopa.Id,
                    EventId = odessa_beach.Id,
                    Role = "MC Styopa"
                });
                db.SaveChanges();
            }

            List<Guest> list = db.EventGuests.Where(eg => eg.Event.Name == "odessa beach").Select(eg => eg.Guest).ToList();

            var eventGuest = db.EventGuests.FirstOrDefault(e => e.GuestId == Styopa.Id && e.EventId == odessa_beach.Id);
            if (eventGuest != null)
            {
                db.EventGuests.Remove(eventGuest);
                db.SaveChanges();
            }
        }
    }
}