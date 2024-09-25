using BookStore.Interfaces;

namespace BookStore.ViewModels;

public class ItemView : IShow<int>
{
    public int Id { get; set; }
    public string Value { get; set; }

    public override string ToString()
    {
        return string.Format("Id: {0}; Value: {1}\n", Id, Value);
    }
}