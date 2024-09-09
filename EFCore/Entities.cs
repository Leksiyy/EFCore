using System.ComponentModel.DataAnnotations.Schema;

namespace EFCore;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    // Связь с товарами
    public List<Product> Products { get; set; }
}
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal PurchasePrice { get; set; }
    public decimal RetailPrice { get; set; }
    public int Quantity { get; set; }
    public string Manufacturer { get; set; }
    public DateTime ExpirationDate { get; set; }

    // Связь с категорией
    public int CategoryId { get; set; }
    public Category Category { get; set; }
}
public class Delivery
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Address { get; set; }
    public string PaymentType { get; set; }
    public string Status { get; set; }
    public string PaymentDetails { get; set; }
    public DateTime ShippingDate { get; set; }
    public DateTime DeliveryDate { get; set; }

    // Связь с пользователем
    public int UserId { get; set; }
    public User User { get; set; }
}
public class User
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    // Связь с доставкой
    public List<Delivery> Deliveries { get; set; }
}