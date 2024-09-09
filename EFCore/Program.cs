using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace EFCore;

class Program
{
    static void Main()
    {
        using (var db = new ApplicationContext())
        {
            var repo = new PerfumeStoreRepository(db);

            repo.AddCategory("Парфюмерия", "Духи, одеколоны и парфюмерная вода.");
            repo.AddCategory("Косметика", "Кремы, лосьоны и другие косметические средства.");

            var categories = repo.GetCategories();

            repo.DeleteCategory(2);

            repo.AddProduct("Chanel COCO", "Классический женский парфюм", 50, 100, 10, "Chanel", DateTime.Now.AddYears(2), 1);
            repo.AddProduct("Jaguar Classic", "Мужская туалетная вода", 40, 80, 15, "Dior", DateTime.Now.AddYears(3), 1);

            var products = repo.GetProducts();
            
            repo.DeleteProduct(2);

            repo.AddUser("ivan@example.com", "password123");
            repo.AddUser("maria@example.com", "password321");

            var users = repo.GetUsers();

            repo.AddDelivery("Иван Иванов", "ул. Ленина, д. 1", "Наличные", "Ожидается", "Крупные", DateTime.Now, DateTime.Now.AddDays(5), 1, 699);

            var deliveries = repo.GetDeliveries();

            repo.DeleteDelivery(1);
            
            var totalOrdersPerUser = repo.GetTotalOrdersPerUser();

            var totalSpentPerUser = repo.GetTotalSpentPerUser();

            var mostExpensiveProductPerUser = repo.GetMostExpensiveProductPerUser();
        }
    }
}


 
//Моему клиенту требуется база данных по управлению «Магазина парфюмерной продукции». В базе данных должно быть: 
//  
// 1) Категории: Название, Описание. 
// 2) Товары: Название, описание, закупочная цена, розничная цена, количество, производитель, срок годности. 
// 3) Доставка: Фио, адрес, тип платежа, статус, реквизиты, дата отправки, дата получения. 
// 4) Пользователи: Все данные по доставке, email, пароль. Возможность просмотров и удаление заказов.
//  
// Реализуйте необходимый функционал, используя классы и методы. Протестируйте ваше приложение. Для удобства, можете оформить все в отдельную библиотеку классов.

//Общее количество заказов для каждого клиента.
// Общая сумма, потраченная каждым клиентов.
// Самый дорогой товар, купленный каждым клиентом.