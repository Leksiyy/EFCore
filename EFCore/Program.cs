using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace EFCore;

class Program
{
    static void Main()
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            // новая категория
            var category = new Category { Name = "Парфюмерия", Description = "Духи, одеколоны и т.д." };
            db.Categories.Add(category);
            db.SaveChanges();

            // новый продукт
            var product = new Product
            {
                Name = "Chanel No 5",
                Description = "Классический женский парфюм",
                PurchasePrice = 50,
                RetailPrice = 100,
                Quantity = 10,
                Manufacturer = "Chanel",
                ExpirationDate = DateTime.Now.AddYears(2),
                CategoryId = category.Id
            };
            db.Products.Add(product);
            db.SaveChanges();

            // новый пользователь и доставка
            var user = new User { Email = "user@gmail.com", Password = "password" };
            var delivery = new Delivery
            {
                FullName = "Иван Иванов",
                Address = "ул. Ленина, д. 1",
                PaymentType = "Наличные",
                Status = "Ожидается",
                PaymentDetails = "Безналичный расчет",
                ShippingDate = DateTime.Now,
                DeliveryDate = DateTime.Now.AddDays(5),
                User = user
            };
            user.Deliveries.Add(delivery);
            db.Users.Add(user);
            db.SaveChanges();

            // категории
            var categories = db.Categories.ToList();

            // товары
            var products = db.Products.ToList();
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