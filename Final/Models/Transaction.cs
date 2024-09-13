namespace Final.Models;

public class Transaction
{
    public int Id { get; set; }
    public Type Type { get; set; } // доход, расход
    public decimal Amount { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public ICollection<Category> Category { get; set; } = new List<Category>();
}