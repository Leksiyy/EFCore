using BookStore.Models;

namespace BookStore.Interfaces;

public interface IAuthor
{
    Task<IEnumerable<Author>> GetAllAuthorsAsync();
    Task<Author> GetAuthorWhithBooksAsync(int id);
    Task<Author> GetAuthorAsync(int id);
    Task<IEnumerable<Author>> GetAuthorsByNameAsync(string name);

    Task AddAuthorAsync(Author author);
    Task DeleteAuthorAsync(Author author);
    Task EditAuthorAsync(Author author);
}