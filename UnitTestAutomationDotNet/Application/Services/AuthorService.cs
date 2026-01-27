using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class AuthorService
{
    private readonly IAuthorRepository _authorRepository;

    public AuthorService(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository ?? throw new ArgumentNullException(nameof(authorRepository));
    }

    public async Task<Author> CreateAuthorAsync(string name, string email, string bio)
    {
        // Check if email already exists
        if (await _authorRepository.EmailExistsAsync(email))
        {
            throw new InvalidOperationException($"An author with email '{email}' already exists");
        }

        var author = new Author(name, email, bio);
        await _authorRepository.AddAsync(author);
        return author;
    }

    public async Task<Author?> GetAuthorByIdAsync(Guid id)
    {
        return await _authorRepository.GetByIdAsync(id);
    }

    public async Task<Author?> GetAuthorByEmailAsync(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be empty", nameof(email));

        return await _authorRepository.GetByEmailAsync(email);
    }

    public async Task<IEnumerable<Author>> GetAllActiveAuthorsAsync()
    {
        return await _authorRepository.GetAllActiveAsync();
    }

    public async Task<IEnumerable<Author>> GetAllAuthorsAsync()
    {
        return await _authorRepository.GetAllAsync();
    }

    public async Task UpdateAuthorNameAsync(Guid id, string newName)
    {
        var author = await _authorRepository.GetByIdAsync(id);
        if (author == null)
            throw new InvalidOperationException($"Author with id '{id}' not found");

        author.UpdateName(newName);
        await _authorRepository.UpdateAsync(author);
    }

    public async Task UpdateAuthorEmailAsync(Guid id, string newEmail)
    {
        var author = await _authorRepository.GetByIdAsync(id);
        if (author == null)
            throw new InvalidOperationException($"Author with id '{id}' not found");

        // Check if new email is already taken by another author
        var existingAuthor = await _authorRepository.GetByEmailAsync(newEmail);
        if (existingAuthor != null && existingAuthor.Id != id)
        {
            throw new InvalidOperationException($"Email '{newEmail}' is already taken by another author");
        }

        author.UpdateEmail(newEmail);
        await _authorRepository.UpdateAsync(author);
    }

    public async Task UpdateAuthorBioAsync(Guid id, string newBio)
    {
        var author = await _authorRepository.GetByIdAsync(id);
        if (author == null)
            throw new InvalidOperationException($"Author with id '{id}' not found");

        author.UpdateBio(newBio);
        await _authorRepository.UpdateAsync(author);
    }

    public async Task DeactivateAuthorAsync(Guid id)
    {
        var author = await _authorRepository.GetByIdAsync(id);
        if (author == null)
            throw new InvalidOperationException($"Author with id '{id}' not found");

        author.Deactivate();
        await _authorRepository.UpdateAsync(author);
    }

    public async Task ActivateAuthorAsync(Guid id)
    {
        var author = await _authorRepository.GetByIdAsync(id);
        if (author == null)
            throw new InvalidOperationException($"Author with id '{id}' not found");

        author.Activate();
        await _authorRepository.UpdateAsync(author);
    }
}
