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
        
        enum ShopMenu
        {
            Books, Authors, Categories, Orders, SearchAuthors, SearchBooks, SearchCategories, SearchOrders, AddBook, AddAuthor, AddCategory, AddOrder, Exit
        }
        
        static async Task Main()
        {
            Initialize();

            int input = ConsoleHelper.MultipleChoice(true, new ShopMenu());
        }
        static void Initialize()
        {
            new DbInit().Init(DbContext());
            _books = new BookRepository();
        }
    }
}
