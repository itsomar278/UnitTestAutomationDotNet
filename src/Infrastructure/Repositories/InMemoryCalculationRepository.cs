using Domain.Entities;
using Domain.Interfaces;
using Domain.ValueObjects;

namespace Infrastructure.Repositories;

public class InMemoryCalculationRepository : ICalculationRepository
{
    private readonly List<Calculation> _calculations = new();

    public Task<Calculation?> GetByIdAsync(Guid id)
    {
        var calculation = _calculations.FirstOrDefault(c => c.Id == id);
        return Task.FromResult(calculation);
    }

    public Task<IEnumerable<Calculation>> GetBySessionIdAsync(Guid sessionId)
    {
        var calculations = _calculations.Where(c => c.SessionId == sessionId).ToList();
        return Task.FromResult<IEnumerable<Calculation>>(calculations);
    }

    public Task<IEnumerable<Calculation>> GetByOperationTypeAsync(OperationType operationType)
    {
        var calculations = _calculations.Where(c => c.Operation == operationType).ToList();
        return Task.FromResult<IEnumerable<Calculation>>(calculations);
    }

    public Task<IEnumerable<Calculation>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<Calculation>>(_calculations.ToList());
    }

    public Task AddAsync(Calculation calculation)
    {
        if (calculation == null)
        {
            throw new ArgumentNullException(nameof(calculation));
        }

        if (_calculations.Any(c => c.Id == calculation.Id))
        {
            throw new InvalidOperationException($"Calculation with id {calculation.Id} already exists");
        }

        _calculations.Add(calculation);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Calculation calculation)
    {
        if (calculation == null)
        {
            throw new ArgumentNullException(nameof(calculation));
        }

        var existing = _calculations.FirstOrDefault(c => c.Id == calculation.Id);
        if (existing == null)
        {
            throw new InvalidOperationException($"Calculation with id {calculation.Id} not found");
        }

        // In-memory, object is already updated by reference
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Guid id)
    {
        var calculation = _calculations.FirstOrDefault(c => c.Id == id);
        if (calculation == null)
        {
            throw new InvalidOperationException($"Calculation with id {id} not found");
        }

        _calculations.Remove(calculation);
        return Task.CompletedTask;
    }
}
