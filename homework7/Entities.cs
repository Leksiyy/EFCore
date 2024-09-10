namespace homework7;

public class Client
{
    public int ClientId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public List<Order> Orders { get; set; }
}
public class Product
{
    public int ProductId { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public List<OrderDetail> OrderDetail { get; set; }
}
public class Order
{
    public int OrderId { get; set; }
    public int ClientId { get; set; }
    public Client Client { get; set; }
    public DateTime OrderDate { get; set; }
    public string Address { get; set; }
    public List<OrderDetail> OrderDetail { get; set; }
}
public class OrderDetail
{
    public int OrderDetailId { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public int OrderId { get; set; }
    public Order Order { get; set; }
    public int Quantity { get; set; }
}

