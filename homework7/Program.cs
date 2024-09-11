using Microsoft.EntityFrameworkCore;

namespace homework7;

class Program
{
    static void Main(string[] args)
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            var linq1 = db.Clients
                .Include(e => e.Orders)
                .Select(e => new
                {
                    Client = e,
                    TotalOrderCount = e.Orders.Count,
                    TotalSpend = e.Orders
                        .SelectMany(e => e.OrderDetail)
                        .Sum(e => e.Product.Price * e.Quantity),
                    MostExpensiveProduct = e.Orders
                        .SelectMany(e => e.OrderDetail)
                        .Select(e => e.Product.Price)
                        .Max()
                })
                .ToList();
        }
    }
}

//Вам необходимо написать один запрос LINQ to Entities, который извлекает список клиентов вместе со следующей информацией:
// 
// Общее количество заказов для каждого клиента.
// Общая сумма, потраченная каждым клиентов.
// Самый дорогой товар, купленный каждым клиентом.
// 

//Запрос должен вернуть список объектов со следующими свойствами:
// 
// Имя клиента
// Электронная почта
// Адрес
// Общее количество заказов
// Общая потраченная сумма
// Название самого дорогого приобретенного товара