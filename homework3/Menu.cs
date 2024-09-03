namespace homework3;

public static class Menu
{
    public static void MainMenu()
    {
        while (Convert.ToBoolean(1))
        {
            Console.WriteLine("1. Registration\n2. Login\n3. Exit\nEnter your choice:");
            short.TryParse(Console.ReadLine(), out short choose);
            if (choose == 1)
            {
                Registration();
            } else if (choose == 2)
            {
                Login();
            } else if (choose == 3)
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid input, try one more time");
            }
        }
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
        Console.WriteLine("Enter your login:");
        string login = Console.ReadLine();
        
        using (ApplicationContext db = new ApplicationContext())
        {
            User? user = db.Users.FirstOrDefault(e => e.Login == login);
            if (user != null)
            {
                Console.WriteLine("Enter your password:");
                string password = Console.ReadLine();
                if (PasswordHashBuilder.VerifyPassword(password, user.Password))
                {
                    Console.WriteLine("Successfully login.");
                } else Console.WriteLine("Incorrect password.");
            } else Console.WriteLine("User not found.");
        }
    }
}