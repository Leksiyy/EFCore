using Microsoft.EntityFrameworkCore;

namespace EFCore;

public class PerfumeStoreRepository
{
    private readonly ApplicationContext _db;

    public PerfumeStoreRepository(ApplicationContext db)
    {
        _db = db;
    }

    // --- категории ---
    public void AddCategory(string name, string description)
    {
        var category = new Category { Name = name, Description = description };
        _db.Categories.Add(category);
        _db.SaveChanges();
    }

    public List<Category> GetCategories()
    {
        return _db.Categories.ToList();
    }

    public void DeleteCategory(int id)
    {
        var category = _db.Categories.Find(id);
        if (category != null)
        {
            _db.Categories.Remove(category);
            _db.SaveChanges();
        }
    }

    // --- товары ---
    public void AddProduct(string name, string description, decimal purchasePrice, decimal retailPrice, int quantity, string manufacturer, DateTime expirationDate, int categoryId)
    {
        var product = new Product
        {
            Name = name,
            Description = description,
            PurchasePrice = purchasePrice,
            RetailPrice = retailPrice,
            Quantity = quantity,
            Manufacturer = manufacturer,
            ExpirationDate = expirationDate,
            CategoryId = categoryId
        };
        _db.Products.Add(product);
        _db.SaveChanges();
    }

    public List<Product> GetProducts()
    {
        return _db.Products.Include(p => p.Category).ToList();
    }

    public void DeleteProduct(int id)
    {
        var product = _db.Products.Find(id);
        if (product != null)
        {
            _db.Products.Remove(product);
            _db.SaveChanges();
        }
    }

    // --- доставка ---
    public void AddDelivery(string fullName, string address, string paymentType, string status, string paymentDetails, DateTime shippingDate, DateTime deliveryDate, int userId, decimal totalAmount)
    {
        var delivery = new Delivery
        {
            FullName = fullName,
            Address = address,
            PaymentType = paymentType,
            Status = status,
            PaymentDetails = paymentDetails,
            ShippingDate = shippingDate,
            DeliveryDate = deliveryDate,
            UserId = userId,
            TotalAmount = totalAmount
        };
        _db.Deliveries.Add(delivery);
        _db.SaveChanges();
    }

    public List<Delivery> GetDeliveries()
    {
        return _db.Deliveries.Include(d => d.User).ToList();
    }

    public void DeleteDelivery(int id)
    {
        var delivery = _db.Deliveries.Find(id);
        if (delivery != null)
        {
            _db.Deliveries.Remove(delivery);
            _db.SaveChanges();
        }
    }

    // --- пользователи ---
    public void AddUser(string email, string password)
    {
        var user = new User { Email = email, Password = password };
        _db.Users.Add(user);
        _db.SaveChanges();
    }

    public List<User> GetUsers()
    {
        return _db.Users.Include(u => u.Deliveries).ToList();
    }

    public void DeleteUser(int id)
    {
        var user = _db.Users.Find(id);
        if (user != null)
        {
            _db.Users.Remove(user);
            _db.SaveChanges();
        }
    }
    
    public Dictionary<string, int> GetTotalOrdersPerUser()
    {
        return _db.Users
            .Select(u => new
            {
                u.Email,
                TotalOrders = u.Deliveries.Count
            })
            .ToDictionary(x => x.Email, x => x.TotalOrders);
    }

    public Dictionary<string, decimal> GetTotalSpentPerUser()
    {
        return _db.Users
            .Select(u => new
            {
                u.Email,
                TotalSpent = u.Deliveries.Sum(d => d.TotalAmount)
            })
            .ToDictionary(x => x.Email, x => x.TotalSpent);
    }

    public Dictionary<string, Product> GetMostExpensiveProductPerUser()
    {
        var productPurchases = _db.Deliveries
            .Include(d => d.User)
            .Include(d => d.Products)
            .SelectMany(d => d.Products.Select(p => new
            {
                d.User.Email,
                Product = p
            }))
            .GroupBy(x => x.Email)
            .Select(g => new
            {
                Email = g.Key,
                MostExpensiveProduct = g.OrderByDescending(x => x.Product.RetailPrice).FirstOrDefault().Product
            })
            .ToDictionary(x => x.Email, x => x.MostExpensiveProduct);

        return productPurchases;
    }
}
