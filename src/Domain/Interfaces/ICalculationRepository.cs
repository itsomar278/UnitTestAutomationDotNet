using Domain.Entities;
using Domain.ValueObjects;

namespace Domain.Interfaces;

public interface ICalculationRepository
{
    Task<Calculation?> GetByIdAsync(Guid id);
    Task<IEnumerable<Calculation>> GetBySessionIdAsync(Guid sessionId);
    Task<IEnumerable<Calculation>> GetByOperationTypeAsync(OperationType operationType);
    Task<IEnumerable<Calculation>> GetAllAsync();
    Task AddAsync(Calculation calculation);
    Task UpdateAsync(Calculation calculation);
    Task DeleteAsync(Guid id);
}
