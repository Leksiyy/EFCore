namespace homework3;

public static class Menu
{
    public static void MainMenu()
    {
        Console.WriteLine("1. Registration\n2. Login\n3. Exit\nEnter your choice:");
        short.TryParse(Console.ReadLine(), out short choose);
    }

    public static void Registration()
    {
        Console.WriteLine("Enter your login.");
        string login = Console.ReadLine();
        Console.WriteLine("Enter your password.");
        string password = Console.ReadLine();

        if (login.Length > 0 && password.Length > 0)
        {
            string hash = PasswordHashBuilder.HashPasswordWithSalt(password);
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Users.Add(new User
                {
                    Login = login,
                    Password = hash
                });
                db.SaveChanges();
            }
        }
    }
    public static void Login()
    {
        Console.WriteLine("Enter your login.");
        string login = Console.ReadLine();
        Console.WriteLine("Enter your password.");
        string password = Console.ReadLine();
        
        using (ApplicationContext db = new ApplicationContext())
        {
            db.Users.Where(e => e.Login == login);
        }
    }
}