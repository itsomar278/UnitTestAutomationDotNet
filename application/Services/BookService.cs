using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class BookService
{
    private readonly IBookRepository _bookRepository;

    public BookService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
    }

    public async Task<Book> CreateBookAsync(string title, string author, string isbn, int publicationYear)
    {
        var existingBook = await _bookRepository.GetByISBNAsync(isbn);
        if (existingBook != null)
            throw new InvalidOperationException($"Book with ISBN {isbn} already exists");

        var book = new Book(Guid.NewGuid(), title, author, isbn, publicationYear);
        return await _bookRepository.AddAsync(book);
    }

    public async Task<Book?> GetBookByIdAsync(Guid id)
    {
        return await _bookRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Book>> GetAllBooksAsync()
    {
        return await _bookRepository.GetAllAsync();
    }

    public async Task<IEnumerable<Book>> GetAvailableBooksAsync()
    {
        return await _bookRepository.GetAvailableBooksAsync();
    }

    public async Task BorrowBookAsync(Guid bookId)
    {
        var book = await _bookRepository.GetByIdAsync(bookId);
        if (book == null)
            throw new InvalidOperationException($"Book with ID {bookId} not found");

        book.BorrowBook();
        await _bookRepository.UpdateAsync(book);
    }

    public async Task ReturnBookAsync(Guid bookId)
    {
        var book = await _bookRepository.GetByIdAsync(bookId);
        if (book == null)
            throw new InvalidOperationException($"Book with ID {bookId} not found");

        book.ReturnBook();
        await _bookRepository.UpdateAsync(book);
    }
}
