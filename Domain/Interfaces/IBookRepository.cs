using Domain.Entities;

namespace Domain.Interfaces;

public interface IBookRepository
{
    Task<Book?> GetByIdAsync(Guid id);
    Task<IEnumerable<Book>> GetAllAsync();
    Task<IEnumerable<Book>> GetAvailableBooksAsync();
    Task<Book?> GetByISBNAsync(string isbn);
    Task<Book> AddAsync(Book book);
    Task UpdateAsync(Book book);
}
