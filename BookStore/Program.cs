using BookStore.Data;
using BookStore.Helpers;
using BookStore.Interfaces;
using BookStore.Repository;

namespace BookStore
{
    public partial class Program
    {
        public static ApplicationContext DbContext() => new ApplicationContextFactory().CreateDbContext();
        private static IBook _books;
        private static IAuthor _authors;
        private static ICategory _categories;
        private static IOrder _orders;
        
        enum ShopMenu
        {
            Books, Authors, Categories, Orders, SearchAuthors, SearchBooks, SearchCategories, SearchOrders, AddBook, AddAuthor, AddCategory, AddOrder, Exit
        }
        
        static async Task Main()
        { 
            // Initialize();

            int input = new int();
            do
            {
                input = ConsoleHelper.MultipleChoice(true, new ShopMenu());

                switch ((ShopMenu)input)
                {
                    case ShopMenu.Books:
                        await BookService.ReviewBooks();
                        break;
                    case ShopMenu.Authors:
                        await AuthorService.ReviewAuthors();
                        break; 
                    case ShopMenu.Categories:
                        await CategoryService.ReviewCategory();
                        break;
                    case ShopMenu.Orders:
                        await OrderService.ReviewOrders();
                        break;
                    case ShopMenu.SearchAuthors:
                        await AuthorService.SearchAuthor();
                        break;
                    case ShopMenu.SearchBooks:
                        await BookService.SearchBook();
                        break;
                    case ShopMenu.SearchCategories:
                        await CategoryService.SearchCategory();
                        break;
                    case ShopMenu.SearchOrders:
                        await OrderService.SearchOrder();
                        break;
                    case ShopMenu.AddBook:
                        await BookService.AddBook();
                        break;  
                    case ShopMenu.AddAuthor:
                        await AuthorService.AddAuthor();
                        break;
                    case ShopMenu.AddCategory:
                        await CategoryService.AddCategory();
                        break;
                    case ShopMenu.AddOrder:
                        await OrderService.AddOrder();
                        break;
                    case ShopMenu.Exit:
                        break;
                }
                
            } while (true);
        }
        static void Initialize()
        {
            new DbInit().Init(DbContext());
            _books = new BookRepository();
            _authors = new AuthorRepository();
        }
    }
}
