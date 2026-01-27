namespace Application.DTOs;

public record SessionDto(
    Guid Id,
    string Name,
    DateTime CreatedAt,
    DateTime? ClosedAt,
    bool IsActive,
    int CalculationCount);

public record CreateSessionDto(string Name);

public record SessionSummaryDto(
    Guid Id,
    string Name,
    bool IsActive,
    int CalculationCount);
