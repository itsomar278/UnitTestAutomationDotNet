using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class SessionService
{
    private readonly ICalculationSessionRepository _sessionRepository;

    public SessionService(ICalculationSessionRepository sessionRepository)
    {
        _sessionRepository = sessionRepository ?? throw new ArgumentNullException(nameof(sessionRepository));
    }

    public async Task<SessionDto> CreateSessionAsync(CreateSessionDto request)
    {
        var session = new CalculationSession(request.Name);
        await _sessionRepository.AddAsync(session);
        return MapToDto(session);
    }

    public async Task<SessionDto?> GetSessionByIdAsync(Guid id)
    {
        var session = await _sessionRepository.GetByIdAsync(id);
        return session == null ? null : MapToDto(session);
    }

    public async Task<IEnumerable<SessionSummaryDto>> GetAllActiveSessionsAsync()
    {
        var sessions = await _sessionRepository.GetAllActiveAsync();
        return sessions.Select(MapToSummaryDto);
    }

    public async Task<IEnumerable<SessionSummaryDto>> GetAllSessionsAsync()
    {
        var sessions = await _sessionRepository.GetAllAsync();
        return sessions.Select(MapToSummaryDto);
    }

    public async Task CloseSessionAsync(Guid id)
    {
        var session = await _sessionRepository.GetByIdAsync(id);
        if (session == null)
        {
            throw new InvalidOperationException($"Session with id '{id}' not found");
        }

        session.CloseSession();
        await _sessionRepository.UpdateAsync(session);
    }

    public async Task ReopenSessionAsync(Guid id)
    {
        var session = await _sessionRepository.GetByIdAsync(id);
        if (session == null)
        {
            throw new InvalidOperationException($"Session with id '{id}' not found");
        }

        session.ReopenSession();
        await _sessionRepository.UpdateAsync(session);
    }

    public async Task RenameSessionAsync(Guid id, string newName)
    {
        var session = await _sessionRepository.GetByIdAsync(id);
        if (session == null)
        {
            throw new InvalidOperationException($"Session with id '{id}' not found");
        }

        session.Rename(newName);
        await _sessionRepository.UpdateAsync(session);
    }

    public async Task DeleteSessionAsync(Guid id)
    {
        var session = await _sessionRepository.GetByIdAsync(id);
        if (session == null)
        {
            throw new InvalidOperationException($"Session with id '{id}' not found");
        }

        await _sessionRepository.DeleteAsync(id);
    }

    private static SessionDto MapToDto(CalculationSession session)
    {
        return new SessionDto(
            session.Id,
            session.Name,
            session.CreatedAt,
            session.ClosedAt,
            session.IsActive,
            session.GetCalculationCount());
    }

    private static SessionSummaryDto MapToSummaryDto(CalculationSession session)
    {
        return new SessionSummaryDto(
            session.Id,
            session.Name,
            session.IsActive,
            session.GetCalculationCount());
    }
}
