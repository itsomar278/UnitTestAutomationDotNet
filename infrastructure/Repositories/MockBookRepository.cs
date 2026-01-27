using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Repositories;

public class MockBookRepository : IBookRepository
{
    private readonly List<Book> _books;

    public MockBookRepository()
    {
        _books = new List<Book>
        {
            new Book(Guid.Parse("11111111-1111-1111-1111-111111111111"),
                "Clean Code", "Robert C. Martin", "978-0132350884", 2008),
            new Book(Guid.Parse("22222222-2222-2222-2222-222222222222"),
                "Domain-Driven Design", "Eric Evans", "978-0321125217", 2003),
            new Book(Guid.Parse("33333333-3333-3333-3333-333333333333"),
                "The Pragmatic Programmer", "Andrew Hunt", "978-0201616224", 1999)
        };
    }

    public Task<Book?> GetByIdAsync(Guid id)
    {
        var book = _books.FirstOrDefault(b => b.Id == id);
        return Task.FromResult(book);
    }

    public Task<IEnumerable<Book>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<Book>>(_books);
    }

    public Task<IEnumerable<Book>> GetAvailableBooksAsync()
    {
        var availableBooks = _books.Where(b => b.IsAvailable);
        return Task.FromResult<IEnumerable<Book>>(availableBooks);
    }

    public Task<Book?> GetByISBNAsync(string isbn)
    {
        var book = _books.FirstOrDefault(b => b.ISBN == isbn);
        return Task.FromResult(book);
    }

    public Task<Book> AddAsync(Book book)
    {
        _books.Add(book);
        return Task.FromResult(book);
    }

    public Task UpdateAsync(Book book)
    {
        var existingBook = _books.FirstOrDefault(b => b.Id == book.Id);
        if (existingBook != null)
        {
            _books.Remove(existingBook);
            _books.Add(book);
        }
        return Task.CompletedTask;
    }
}
