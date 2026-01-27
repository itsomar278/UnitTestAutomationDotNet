using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Repositories;

public class InMemorySessionRepository : ICalculationSessionRepository
{
    private readonly List<CalculationSession> _sessions = new();

    public Task<CalculationSession?> GetByIdAsync(Guid id)
    {
        var session = _sessions.FirstOrDefault(s => s.Id == id);
        return Task.FromResult(session);
    }

    public Task<IEnumerable<CalculationSession>> GetAllActiveAsync()
    {
        var sessions = _sessions.Where(s => s.IsActive).ToList();
        return Task.FromResult<IEnumerable<CalculationSession>>(sessions);
    }

    public Task<IEnumerable<CalculationSession>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<CalculationSession>>(_sessions.ToList());
    }

    public Task AddAsync(CalculationSession session)
    {
        if (session == null)
        {
            throw new ArgumentNullException(nameof(session));
        }

        if (_sessions.Any(s => s.Id == session.Id))
        {
            throw new InvalidOperationException($"Session with id {session.Id} already exists");
        }

        _sessions.Add(session);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(CalculationSession session)
    {
        if (session == null)
        {
            throw new ArgumentNullException(nameof(session));
        }

        var existing = _sessions.FirstOrDefault(s => s.Id == session.Id);
        if (existing == null)
        {
            throw new InvalidOperationException($"Session with id {session.Id} not found");
        }

        // In-memory, object is already updated by reference
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Guid id)
    {
        var session = _sessions.FirstOrDefault(s => s.Id == id);
        if (session == null)
        {
            throw new InvalidOperationException($"Session with id {id} not found");
        }

        _sessions.Remove(session);
        return Task.CompletedTask;
    }
}
