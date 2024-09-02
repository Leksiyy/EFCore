namespace homework2;

public class Product
{
    public int Id { get; set; }
    public string Category { get; set; }
    public decimal Price { get; set; }
    public int? Weight { get; set; }
    public List<Order> Orders { get; set; } = new List<Order>();
}

public class Order
{
    public int Id { get; set; }
    public DateTime OrderTime { get; set; }
    public int PaymentMethod { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
}

