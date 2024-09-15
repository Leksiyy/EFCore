using System.Numerics;

namespace BookStore.Interfaces;

public interface IShow<T> where T : INumber<int>
{
    T Id { get; set; }
    string Value { get; set; }
}