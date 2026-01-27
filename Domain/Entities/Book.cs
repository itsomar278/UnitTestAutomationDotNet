namespace Domain.Entities;

public class Book
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string Author { get; private set; }
    public string ISBN { get; private set; }
    public int PublicationYear { get; private set; }
    public bool IsAvailable { get; private set; }
    public string Description { get; private set; }

    private Book()
    {
        Title = string.Empty;
        Author = string.Empty;
        ISBN = string.Empty;
        Description = string.Empty;
    }

    public Book(Guid id, string title, string author, string isbn, int publicationYear)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty", nameof(title));

        if (string.IsNullOrWhiteSpace(author))
            throw new ArgumentException("Author cannot be empty", nameof(author));

        if (string.IsNullOrWhiteSpace(isbn))
            throw new ArgumentException("ISBN cannot be empty", nameof(isbn));

        Id = id;
        Title = title;
        Author = author;
        ISBN = isbn;
        PublicationYear = publicationYear;
        IsAvailable = true;
        Description = string.Empty;
    }

    public void BorrowBook()
    {
        if (!IsAvailable)
            throw new InvalidOperationException("Book is already borrowed");

        IsAvailable = false;
    }

    public void ReturnBook()
    {
        if (IsAvailable)
            throw new InvalidOperationException("Book is already available");

        IsAvailable = true;
    }

    public void UpdateDescription(string description)
    {
        if (description == null)
            throw new ArgumentNullException(nameof(description));

        Description = description;
    }
}
