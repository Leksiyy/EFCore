namespace EFCore;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public UserSettings UserSettings { get; set; }
}

public class UserSettings
{
    public int GuiScale { get; set; }
    public User User { get; set; }
    public int UserId { get; set; }
}