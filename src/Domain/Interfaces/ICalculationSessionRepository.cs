using Domain.Entities;

namespace Domain.Interfaces;

public interface ICalculationSessionRepository
{
    Task<CalculationSession?> GetByIdAsync(Guid id);
    Task<IEnumerable<CalculationSession>> GetAllActiveAsync();
    Task<IEnumerable<CalculationSession>> GetAllAsync();
    Task AddAsync(CalculationSession session);
    Task UpdateAsync(CalculationSession session);
    Task DeleteAsync(Guid id);
}
