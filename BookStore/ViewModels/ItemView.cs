using BookStore.Interfaces;

namespace BookStore.ViewModels;

public class ItemView : IShow<int>
{
    public int Id { get; set; }
    public string Value { get; set; }
}