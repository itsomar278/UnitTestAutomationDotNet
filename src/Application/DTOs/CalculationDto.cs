using Domain.ValueObjects;

namespace Application.DTOs;

public record CalculationDto(
    Guid Id,
    Guid SessionId,
    OperationType Operation,
    double Operand1,
    double Operand2,
    double Result,
    DateTime Timestamp,
    bool IsValid);

public record CreateCalculationDto(
    Guid SessionId,
    OperationType Operation,
    double Operand1,
    double Operand2);

public record CalculationResultDto(
    Guid CalculationId,
    double Result,
    bool Success,
    string? ErrorMessage = null);
