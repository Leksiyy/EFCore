namespace Final.Models;

public class Settings
{
    public int SettingsId { get; set; }
    public bool ShowNotifications { get; set; }
    public int UIScale { get; set; }
    public User User { get; set; }
}