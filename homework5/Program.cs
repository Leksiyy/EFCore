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
                Location = "downtown",
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
            
            // 1) Добавление гостя на событие.
            var Styopa = db.Guests.FirstOrDefault(e => e.Name == "Styopa");
            var odessa_beach = db.Events.FirstOrDefault(e => e.Location == "downtown");

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

            // 2) Получение списка гостей на конкретном событии.
            List<Guest> list = db.EventGuests.Where(eg => eg.Event.Location == "odessa beach")
                .Select(eg => eg.Guest).ToList();
            
            // 3) Изменение роли гостя на событии.
            Guest guest = db.Guests.FirstOrDefault(g => g.Id == 3);
            Event @event = db.Events.FirstOrDefault(e => e.Id == 1);

            if (guest != null && @event != null)
            {
                db.EventGuests.Add(new EventGuest
                {
                    GuestId = guest.Id,
                    EventId = @event.Id,
                    Role = "Speaker",
                });

                db.SaveChanges();
            }

            EventGuest editIt = db.EventGuests.FirstOrDefault(e => e.GuestId == guest.Id && e.EventId == @event.Id);
            editIt.Role = "speaker"; // в задании нужно было изменить, поэтому сначала добавляю, потом изменяю
            db.SaveChanges();
            
            // 4) Получение всех событий для конкретного гостя.
            List<Event> list1 = db.EventGuests
                .Where(e => e.GuestId == guest.Id)
                .Include(e => e.Event)
                .Select(e => e.Event)
                .ToList();
            
            // 5) Удаление гостя с события.
            var eventGuest = db.EventGuests
                .FirstOrDefault(e => e.GuestId == Styopa.Id && e.EventId == odessa_beach.Id);
            
            if (eventGuest != null)
            {
                db.EventGuests.Remove(eventGuest);
                db.SaveChanges();
            }
            
            // 6) Получение всех событий, на которых гость выступал в роли спикера.
            List<Event> list3 = db.EventGuests
                .Where(e => e.GuestId == guest.Id && e.Role == "speaker")
                .Include(e => e.Event)
                .Select(e => e.Event)
                .ToList();
        }
    }
}