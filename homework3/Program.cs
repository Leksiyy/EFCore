namespace homework3;

class Program
{
    static void Main(string[] args)
    {
        ApplicationContext.RecreateDB();
        Menu.MainMenu();
    }
}

