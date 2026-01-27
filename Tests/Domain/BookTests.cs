using Domain.Entities;

namespace Tests.Domain;

public class BookTests
{
    [Fact]
    public void CreateBook_WithValidData_ShouldCreateBook()
    {
        var id = Guid.NewGuid();
        var book = new Book(id, "Test Book", "Test Author", "978-0132350884", 2020);

        Assert.Equal(id, book.Id);
        Assert.Equal("Test Book", book.Title);
        Assert.Equal("Test Author", book.Author);
        Assert.Equal("978-0132350884", book.ISBN);
        Assert.Equal(2020, book.PublicationYear);
        Assert.True(book.IsAvailable);
    }

    [Fact]
    public void CreateBook_WithEmptyTitle_ShouldThrowException()
    {
        var exception = Assert.Throws<ArgumentException>(() =>
            new Book(Guid.NewGuid(), "", "Author", "ISBN", 2020));

        Assert.Equal("Title cannot be empty (Parameter 'title')", exception.Message);
    }

    [Fact]
    public void CreateBook_WithEmptyAuthor_ShouldThrowException()
    {
        var exception = Assert.Throws<ArgumentException>(() =>
            new Book(Guid.NewGuid(), "Title", "", "ISBN", 2020));

        Assert.Equal("Author cannot be empty (Parameter 'author')", exception.Message);
    }

    [Fact]
    public void CreateBook_WithEmptyISBN_ShouldThrowException()
    {
        var exception = Assert.Throws<ArgumentException>(() =>
            new Book(Guid.NewGuid(), "Title", "Author", "", 2020));

        Assert.Equal("ISBN cannot be empty (Parameter 'isbn')", exception.Message);
    }

    [Fact]
    public void BorrowBook_WhenAvailable_ShouldMakeBookUnavailable()
    {
        var book = new Book(Guid.NewGuid(), "Test Book", "Test Author", "ISBN", 2020);

        book.BorrowBook();

        Assert.False(book.IsAvailable);
    }

    [Fact]
    public void BorrowBook_WhenAlreadyBorrowed_ShouldThrowException()
    {
        var book = new Book(Guid.NewGuid(), "Test Book", "Test Author", "ISBN", 2020);
        book.BorrowBook();

        var exception = Assert.Throws<InvalidOperationException>(() => book.BorrowBook());

        Assert.Equal("Book is already borrowed", exception.Message);
    }

    [Fact]
    public void ReturnBook_WhenBorrowed_ShouldMakeBookAvailable()
    {
        var book = new Book(Guid.NewGuid(), "Test Book", "Test Author", "ISBN", 2020);
        book.BorrowBook();

        book.ReturnBook();

        Assert.True(book.IsAvailable);
    }

    [Fact]
    public void ReturnBook_WhenAlreadyAvailable_ShouldThrowException()
    {
        var book = new Book(Guid.NewGuid(), "Test Book", "Test Author", "ISBN", 2020);

        var exception = Assert.Throws<InvalidOperationException>(() => book.ReturnBook());

        Assert.Equal("Book is already available", exception.Message);
    }
}
