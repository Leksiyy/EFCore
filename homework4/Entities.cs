namespace homework4;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public UserSettings UserSettings { get; set; }
}

public class UserSettings
{
    public int Id { get; set; }
    public int GuiScale { get; set; }
    public User User { get; set; }
    public int UserId { get; set; }
}