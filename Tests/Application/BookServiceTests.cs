using Application.Services;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Repositories;

namespace Tests.Application;

public class BookServiceTests
{
    private readonly IBookRepository _repository;
    private readonly BookService _bookService;

    public BookServiceTests()
    {
        _repository = new MockBookRepository();
        _bookService = new BookService(_repository);
    }

    [Fact]
    public async Task GetAllBooksAsync_ShouldReturnAllBooks()
    {
        var books = await _bookService.GetAllBooksAsync();

        Assert.NotEmpty(books);
        Assert.Equal(3, books.Count());
    }

    [Fact]
    public async Task GetAvailableBooksAsync_ShouldReturnOnlyAvailableBooks()
    {
        var books = await _bookService.GetAvailableBooksAsync();

        Assert.NotEmpty(books);
        Assert.All(books, book => Assert.True(book.IsAvailable));
    }

    [Fact]
    public async Task GetBookByIdAsync_WithValidId_ShouldReturnBook()
    {
        var bookId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        var book = await _bookService.GetBookByIdAsync(bookId);

        Assert.NotNull(book);
        Assert.Equal(bookId, book.Id);
        Assert.Equal("Clean Code", book.Title);
    }

    [Fact]
    public async Task GetBookByIdAsync_WithInvalidId_ShouldReturnNull()
    {
        var book = await _bookService.GetBookByIdAsync(Guid.NewGuid());

        Assert.Null(book);
    }

    [Fact]
    public async Task CreateBookAsync_WithUniqueISBN_ShouldCreateBook()
    {
        var book = await _bookService.CreateBookAsync(
            "New Book",
            "New Author",
            "978-1234567890",
            2024);

        Assert.NotNull(book);
        Assert.Equal("New Book", book.Title);
        Assert.Equal("New Author", book.Author);
        Assert.True(book.IsAvailable);
    }

    [Fact]
    public async Task CreateBookAsync_WithDuplicateISBN_ShouldThrowException()
    {
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
            _bookService.CreateBookAsync("Title", "Author", "978-0132350884", 2024));

        Assert.Contains("already exists", exception.Message);
    }

    [Fact]
    public async Task BorrowBookAsync_WithValidId_ShouldBorrowBook()
    {
        var bookId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        await _bookService.BorrowBookAsync(bookId);

        var book = await _bookService.GetBookByIdAsync(bookId);
        Assert.NotNull(book);
        Assert.False(book.IsAvailable);
    }

    [Fact]
    public async Task BorrowBookAsync_WithInvalidId_ShouldThrowException()
    {
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
            _bookService.BorrowBookAsync(Guid.NewGuid()));

        Assert.Contains("not found", exception.Message);
    }

    [Fact]
    public async Task ReturnBookAsync_WithBorrowedBook_ShouldReturnBook()
    {
        var bookId = Guid.Parse("22222222-2222-2222-2222-222222222222");
        await _bookService.BorrowBookAsync(bookId);

        await _bookService.ReturnBookAsync(bookId);

        var book = await _bookService.GetBookByIdAsync(bookId);
        Assert.NotNull(book);
        Assert.True(book.IsAvailable);
    }

    [Fact]
    public async Task ReturnBookAsync_WithInvalidId_ShouldThrowException()
    {
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
            _bookService.ReturnBookAsync(Guid.NewGuid()));

        Assert.Contains("not found", exception.Message);
    }
}
