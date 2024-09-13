namespace Final.Models;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public int TransactionId { get; set; }
    public Transaction Transaction { get; set; }
}