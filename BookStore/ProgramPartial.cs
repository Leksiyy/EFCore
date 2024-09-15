using BookStore.Helpers;
using BookStore.Models;
using BookStore.ViewModels;

namespace BookStore;

public partial class Program
{
    public class AuthorService
    {
        public static async Task ReviewAuthors()
        {
            var allAuthors = await _authors.GetAllAuthorsAsync();
            var authors = allAuthors.Select(e => new ItemView { Id = e.Id, Value = e.Name }).ToList();
            int result = ItemHelper.MultipleChoice(true, authors, true);
            if (result != 0)
            {
                var currentAuthor = await _authors.GetAuthorAsync(result);
                await AuthorInfo(currentAuthor);
            }
        }
        public static async Task AuthorInfo(Author currentAuthor)
        {
            int result = ItemHelper.MultipleChoice(true, new List<ItemView>
            {
                new ItemView { Id = 1, Value = "Browse books" },
                new ItemView { Id = 2, Value = "Edit author" },
                new ItemView { Id = 3, Value = "Delete author" },
            }, IsMenu: true, message: String.Format("{0}\n", currentAuthor), startY: 5, optionsPerLine: 1);

            switch (result)
            {
                case 1:
                    // <------------ вызов метода с выводом книг данного автора
                    break;
                case 2:
                    await EditAuthor(currentAuthor);
                    Console.ReadLine();
                    break;
                case 3:
                    await DeleteAuthor(currentAuthor);
                    Console.ReadLine();
                    break;
            }
        }
        public static async Task EditAuthor(Author author)
        {
            Console.WriteLine("Changing : {0}", author.Name);
            author.Name = InputHelper.GetString("author 'Name' with 'SurName'");
            await _authors.EditAuthorAsync(author);
            Console.WriteLine("Done");
        }
        public static async Task AddAuthor()
        {
            string authorName = InputHelper.GetString("author 'Name' with 'SurName'");
            await _authors.AddAuthorAsync(new Author
            {
                Name = authorName
            });
            Console.WriteLine("Done");
        }
        public static async Task DeleteAuthor(Author author)
        {
            int result = ItemHelper.MultipleChoice(true, new List<ItemView>
            {
                new ItemView { Id = 1, Value = "Yes" },
                new ItemView { Id = 0, Value = "No" },
            }, message: String.Format("[Are you sure you want to delete the author {0} ?\n", author.Name), startY: 2);

            if (result == 1)
            {
                await _authors.DeleteAuthorAsync(author);
            }
        }
        public static async Task SearchAuthor()
        {
            string authorName = InputHelper.GetString("author name or surname");
            var currentAuthors = await _authors.GetAuthorsByNameAsync(authorName);
            if (!currentAuthors.Any())
            {
                var authors = currentAuthors.Select(e => new ItemView { Id = e.Id, Value = e.Name }).ToList();
                int result = ItemHelper.MultipleChoice(true, authors, true);
                if (result != 0)
                {
                    var currentAuthor = await _authors.GetAuthorAsync(result);
                    await AuthorInfo(currentAuthor);
                }
            }
            else
            {
                Console.WriteLine("No authors were found by this attribute");
            }
        }
    }

    public class BookService
    {
        public static async Task ReviewBooks()
        {
            var allBooks = await _books.GetAllBooksAsync();
            var books = allBooks.Select(e => new ItemView { Id = e.Id, Value = e.Title }).ToList();
            int input = ItemHelper.MultipleChoice(true, books, true);
            if (input != 0)
            {
                var currentBook = await _books.GetBookAsync(input);
                await BookInfo(currentBook);
            }
        }
        public static async Task BookInfo(Book currentBook)
        {
            int input = ItemHelper.MultipleChoice(true, new List<ItemView>
            {
                new ItemView { Id = 1, Value = "Browse author(s)" },
                new ItemView { Id = 2, Value = "Edit book" },
                new ItemView { Id = 3, Value = "Delete book" },
            }, true, message: String.Format("{0}\n", currentBook.Title), startY: 5, optionsPerLine: 1);

            switch (input)
            {
                case 1:
                    int input2 = ItemHelper.MultipleChoice(true, currentBook.Authors.Select(e => new { Id = e.Id, Value = e.Name }) as List<ItemView>, true);
                    await AuthorService.AuthorInfo(currentBook.Authors.First(e => e.Id == input2));
                    Console.WriteLine();
                    break;
                case 2:
                    await EditBook(currentBook);
                    break;
                case 3:
                    await DeleteBook(currentBook);
                    break;
            }
        }
        public static async Task EditBook(Book currentBook)
        {
            int input = ItemHelper.MultipleChoice(true, new List<ItemView>
            {
                new ItemView { Id = 1, Value = "Edit title" },
                new ItemView { Id = 2, Value = "Edit description" },
                new ItemView { Id = 3, Value = "Edit price" },
            }, true, message: String.Format("{0}\n", currentBook.Title));

            switch (input)
            {
                case 1:
                    string newTitle = InputHelper.GetString("book title");
                    currentBook.Title = newTitle;
                    await _books.EditBookAsync(currentBook);
                    break;
                case 2:
                    string newDesc = InputHelper.GetString("book description");
                    currentBook.Description = newDesc;
                    await _books.EditBookAsync(currentBook);
                    break;
                case 3:
                    decimal newPrice = InputHelper.GetDecimal("book price");
                    currentBook.Price = newPrice;
                    await _books.EditBookAsync(currentBook);
                    break;
            }
        }
        public static async Task AddBook()
        {
            string Title = InputHelper.GetString("book title");
            string Description = InputHelper.GetString("book description");
            decimal Price = InputHelper.GetDecimal("book price");
            DateOnly PublishedOn = InputHelper.GetDate("book date");
            string? Publisher = InputHelper.GetString("book publisher");
            await _books.AddBookAsync(new Book
            {
                Title = Title,
                Description = Description,
                Price = Price,
                Publisher = Publisher,
                PublishedOn = PublishedOn.ToDateTime(TimeOnly.MinValue)
            });
            Console.WriteLine("Book added");
        }
        public static async Task DeleteBook(Book currentBook)
        {
            int result = ItemHelper.MultipleChoice(true, new List<ItemView>
            {
                new ItemView { Id = 1, Value = "Yes" },
                new ItemView { Id = 0, Value = "No" },
            }, message: String.Format("[Are you sure you want to delete the book {0} ?\n", currentBook.Title), startY: 2);

            if (result == 1)
            {
                await _books.DeleteBookAsync(currentBook);
            }
        }
        public static async Task SearchBook()
        {
            string bookName = InputHelper.GetString("book title");
            var currentBooks = await _books.GetBooksByNameAsync(bookName);
            if (!currentBooks.Any())
            {
                var books = currentBooks.Select(e => new ItemView { Id = e.Id, Value = e.Title }).ToList();
                int result = ItemHelper.MultipleChoice(true, books, true);
                if (result != 0)
                {
                    var currentBook = await _books.GetBookAsync(result);
                    await BookInfo(currentBook);
                }
            }
            else
            {
                Console.WriteLine("No authors were found by this attribute");
            }
        }
    }

    public class OrderService
    {
        public static async Task ReviewOrders()
        {
            var allOrders = await _orders.GetAllOrdersAsync();
            var orders = allOrders.Select(e => new ItemView { Id = e.Id, Value = e.ToString() }).ToList();
            int input = ItemHelper.MultipleChoice(true, orders, true);
            if (input != 0)
            {
                var currentOrder = await _orders.GetOrderAsync(input);
                await OrderInfo(currentOrder);
            }
        }
        public static async Task OrderInfo(Order currentOrder)
        {
            int input = ItemHelper.MultipleChoice(true, new List<ItemView>
            {
                new ItemView { Id = 2, Value = "Edit order" },
                new ItemView { Id = 3, Value = "Delete order" },
            }, true, message: String.Format("{0}\n", currentOrder.Id), startY: 5, optionsPerLine: 1);

            switch (input)
            {
                case 1:
                    await EditOrder(currentOrder);
                    break;
                case 2:
                    await DeleteOrder(currentOrder);
                    break;
            }
        }
        public static async Task EditOrder(Order order)
        {
            int input = ItemHelper.MultipleChoice(true, new List<ItemView>
            {
                new ItemView { Id = 1, Value = "Edit customer name" },
                new ItemView { Id = 2, Value = "Edit city" },
                new ItemView { Id = 3, Value = "Edit address" },
            }, true, message: String.Format("{0}\n", order.Id)); // ну а что еще выводить?

            switch (input)
            {
                case 1:
                    string newCustomerName = InputHelper.GetString("customer name");
                    order.CustomerName = newCustomerName;
                    await _orders.UpdateOrderAsync(order);
                    break;
                case 2:
                    string newCity = InputHelper.GetString("city");
                    order.City = newCity;
                    await _orders.UpdateOrderAsync(order);
                    break;
                case 3:
                    string newAddress = InputHelper.GetString("address");
                    order.Address = newAddress;
                    await _orders.UpdateOrderAsync(order);
                    break;
            }
        }
        public static async Task AddOrder()
        {
            string CustomerName = InputHelper.GetString("customer name");
            string City = InputHelper.GetString("city");
            string Address = InputHelper.GetString("address");
            bool Shipped = InputHelper.GetBoolean("shipped");

            await _orders.AddOrderAsync(new Order
            {
                CustomerName = CustomerName,
                Address = Address,
                City = City,
                Shipped = Shipped,
            });
            Console.WriteLine("Order added");
        }
        public static async Task DeleteOrder(Order order)
        {
            int result = ItemHelper.MultipleChoice(true, new List<ItemView>
                {
                    new ItemView { Id = 1, Value = "Yes" },
                    new ItemView { Id = 0, Value = "No" },
                }, message: String.Format("[Are you sure you want to delete the order {0} ?\n", order.Id),
                startY: 2);

            if (result == 1)
            {
                await _orders.DeleteOrderAsync(order);
            }
        }
        public static async Task SearchOrder()
        {
            string orderName = InputHelper.GetString("cusotmer name");
            var currentOrders = await _orders.GetAllOrdersByNameAsync(orderName);
            if (!currentOrders.Any())
            {
                var orders = currentOrders.Select(e => new ItemView { Id = e.Id, Value = e.CustomerName }).ToList();
                int result = ItemHelper.MultipleChoice(true, orders, true);
                if (result != 0)
                {
                    var currentOrder = await _orders.GetOrderAsync(result);
                    await OrderInfo(currentOrder);
                }
            }
            else
            {
                Console.WriteLine("No orders were found by this attribute");
            }
        }
    }

    public class CategoryService
    {
        public static async Task ReviewCategory()
        {
            var allCategories = await _categories.GetAllCategoriesAsync();
            var categories = allCategories.Select(e => new ItemView { Id = e.Id, Value = e.Name }).ToList();
            int input = ItemHelper.MultipleChoice(true, categories, true);
            if (input != 0)
            {
                var currentCategory = await _categories.GetCategoryAsync(input);
                await CategoryInfo(currentCategory);
            }
        }
        public static async Task CategoryInfo(Category currentCategory)
        {
            int input = ItemHelper.MultipleChoice(true, new List<ItemView>
            {
                new ItemView { Id = 2, Value = "Edit category" },
                new ItemView { Id = 3, Value = "Delete category" },
            }, true, message: String.Format("{0}\n", currentCategory.Id), startY: 5, optionsPerLine: 1);

            switch (input)
            {
                case 1:
                    await EditCategory(currentCategory);
                    break;
                case 2:
                    await DeleteCategory(currentCategory);
                    break;
            }
        }
        public static async Task EditCategory(Category category)
        {
            int input = ItemHelper.MultipleChoice(true, new List<ItemView>
            {
                new ItemView { Id = 1, Value = "Edit name" },
                new ItemView { Id = 2, Value = "Edit description" },
            }, true, message: String.Format("{0}\n", category.Name));

            switch (input)
            {
                case 1:
                    string newName = InputHelper.GetString("name");
                    category.Name = newName;
                    await _categories.UpdateCategoryAsync(category);
                    break;
                case 2:
                    string newDescription = InputHelper.GetString("description");
                    category.Description = newDescription;
                    await _categories.UpdateCategoryAsync(category);
                    break;
            }
        }
        public static async Task AddCategory()
        {
            string Name = InputHelper.GetString("name");
            string Description = InputHelper.GetString("description");
            
            await _categories.AddCategoryAsync(new Category
            {
                Name = Name,
                Description = Description
            });
            Console.WriteLine("Category added");
        }
        public static async Task DeleteCategory(Category category)
        {
            int result = ItemHelper.MultipleChoice(true, new List<ItemView>
                {
                    new ItemView { Id = 1, Value = "Yes" },
                    new ItemView { Id = 0, Value = "No" },
                }, message: String.Format("[Are you sure you want to delete the order {0} ?\n", category.Name),
                startY: 2);

            if (result == 1)
            {
                await _categories.DeleteCategoryAsync(category);
            }
        }
        public static async Task SearchCategory()
        {
            string categoryName = InputHelper.GetString("category name");
            var currentCategories = await _categories.GetCategoriesByNameAsync(categoryName);
            if (!currentCategories.Any())
            {
                var orders = currentCategories.Select(e => new ItemView { Id = e.Id, Value = e.Name }).ToList();
                int result = ItemHelper.MultipleChoice(true, orders, true);
                if (result != 0)
                {
                    var currentCategory = await _categories.GetCategoryAsync(result);
                    await CategoryInfo(currentCategory);
                }
            }
            else
            {
                Console.WriteLine("No categories were found by this attribute");
            }
        }
    }
}