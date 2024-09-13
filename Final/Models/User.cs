namespace Final.Models;

public class User
{
    public int Id { get; set; }
    public Settings Settings { get; set; }
    public decimal Balance { get; set; }
    public List<Transaction> Transactions { get; set; }
}