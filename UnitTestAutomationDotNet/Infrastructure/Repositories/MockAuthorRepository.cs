using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Repositories;

public class MockAuthorRepository : IAuthorRepository
{
    private readonly List<Author> _authors;

    public MockAuthorRepository()
    {
        _authors = new List<Author>
        {
            new Author("Robert C. Martin", "uncle.bob@cleancoder.com", "Author of Clean Code and Clean Architecture"),
            new Author("Martin Fowler", "martin@fowler.com", "Chief Scientist at ThoughtWorks, author of Refactoring"),
            new Author("Eric Evans", "eric@domainlanguage.com", "Pioneer of Domain-Driven Design")
        };
    }

    public Task<Author?> GetByIdAsync(Guid id)
    {
        var author = _authors.FirstOrDefault(a => a.Id == id);
        return Task.FromResult(author);
    }

    public Task<Author?> GetByEmailAsync(string email)
    {
        var author = _authors.FirstOrDefault(a =>
            a.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        return Task.FromResult(author);
    }

    public Task<IEnumerable<Author>> GetAllActiveAsync()
    {
        var activeAuthors = _authors.Where(a => a.IsActive).ToList();
        return Task.FromResult<IEnumerable<Author>>(activeAuthors);
    }

    public Task<IEnumerable<Author>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<Author>>(_authors);
    }

    public Task AddAsync(Author author)
    {
        if (author == null)
            throw new ArgumentNullException(nameof(author));

        if (_authors.Any(a => a.Email.Equals(author.Email, StringComparison.OrdinalIgnoreCase)))
            throw new InvalidOperationException($"Author with email {author.Email} already exists");

        _authors.Add(author);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Author author)
    {
        if (author == null)
            throw new ArgumentNullException(nameof(author));

        var existingAuthor = _authors.FirstOrDefault(a => a.Id == author.Id);
        if (existingAuthor == null)
            throw new InvalidOperationException($"Author with id {author.Id} not found");

        // In a real implementation, this would update the database
        // For mock, the object is already updated by reference
        return Task.CompletedTask;
    }

    public Task<bool> EmailExistsAsync(string email)
    {
        var exists = _authors.Any(a =>
            a.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        return Task.FromResult(exists);
    }
}
