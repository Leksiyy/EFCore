using Microsoft.Extensions.Logging;

namespace homework5;

public class Event
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool isPrivate { get; set; }
    public string Location { get; set; }
    public List<EventGuest> EventGuests { get; set; }
}

public class Guest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool isVip { get; set; }
    public List<EventGuest> EventGuests { get; set; }
}

public class EventGuest
{
    public int EventId { get; set; }
    public Event Event { get; set; }
    
    public int GuestId { get; set; }
    public Guest Guest { get; set; }
    
    public string Role { get; set; } // типо foreign key
}