using BookStore.Data;
using BookStore.Helpers;
using BookStore.Interfaces;
using BookStore.Repository;

namespace BookStore
{
    class Program
    {
        public static ApplicationContext DbContext() => new ApplicationContextFactory().CreateDbContext();
        private static IBook _books;
        private static IAuthor _authors;
        private static ICategory _categories;
        private static IOrder _orders;
        
        enum ShopMenu
        {
            Books, Authors, Categories, Orders, SearchAuthors, SearchBooks, SearchCategories, SearchOrders, AddBook, AddAuthor, AddCategory, AddOrder, RemoveBook, RemoveAuthor, RemoveCategory, RemoveOrder, UpdateBook, UpdateAuthor, UpdateCategory, UpdateOrder, Exit
        }
        
        static async Task Main()
        {
            Initialize();
            int input = ConsoleHelper.MultipleChoice(true, new ShopMenu());
            while (true)
            {
                switch (input)
                {
                    case (int)ShopMenu.Books:
                        Console.Clear();
                        Console.WriteLine(_books.GetAllBooksAsync());
                        Console.WriteLine("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case (int)ShopMenu.Authors:
                        Console.Clear();
                        Console.WriteLine(_authors.GetAllAuthorsAsync());
                        Console.WriteLine("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case (int)ShopMenu.Categories:
                        Console.Clear();
                        Console.WriteLine(_categories.GetAllCategoriesAsync());
                        Console.WriteLine("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case (int)ShopMenu.Orders:
                        Console.Clear();
                        Console.WriteLine(_orders.GetAllOrdersAsync());
                        Console.WriteLine("Press any key to continue");
                        Console.ReadKey();
                        break;
                                        
                    //---

                    case (int)ShopMenu.SearchAuthors:
                        Console.Clear();
                        //Console.WriteLine(_authors.GetAllOrdersAsync());
                        Console.WriteLine("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case (int)ShopMenu.SearchBooks:
                        Console.Clear();
                        //Console.WriteLine(_books.GetAllOrdersAsync());
                        Console.WriteLine("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case (int)ShopMenu.SearchCategories:
                        Console.Clear();
                        //Console.WriteLine(_categories.GetAllOrdersAsync());
                        Console.WriteLine("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case (int)ShopMenu.SearchOrders:
                        Console.Clear();
                        Console.WriteLine(_orders.GetAllOrdersAsync());
                        Console.WriteLine("Press any key to continue");
                        Console.ReadKey();
                        break;
                }
            }
        }
        static void Initialize()
        {
            new DbInit().Init(DbContext());
            _books = new BookRepository();
        }
    }
}
