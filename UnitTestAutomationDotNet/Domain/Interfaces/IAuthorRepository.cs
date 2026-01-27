using Domain.Entities;

namespace Domain.Interfaces;

public interface IAuthorRepository
{
    Task<Author?> GetByIdAsync(Guid id);
    Task<Author?> GetByEmailAsync(string email);
    Task<IEnumerable<Author>> GetAllActiveAsync();
    Task<IEnumerable<Author>> GetAllAsync();
    Task AddAsync(Author author);
    Task UpdateAsync(Author author);
    Task<bool> EmailExistsAsync(string email);
}
