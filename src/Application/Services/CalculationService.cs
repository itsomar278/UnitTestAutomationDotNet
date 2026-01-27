using Application.DTOs;
using Calculator;
using Domain.Entities;
using Domain.Interfaces;
using Domain.ValueObjects;

namespace Application.Services;

public class CalculationService
{
    private readonly ICalculationRepository _calculationRepository;
    private readonly ICalculationSessionRepository _sessionRepository;
    private readonly MathCalculator _calculator;

    public CalculationService(
        ICalculationRepository calculationRepository,
        ICalculationSessionRepository sessionRepository)
    {
        _calculationRepository = calculationRepository ?? throw new ArgumentNullException(nameof(calculationRepository));
        _sessionRepository = sessionRepository ?? throw new ArgumentNullException(nameof(sessionRepository));
        _calculator = new MathCalculator();
    }

    public async Task<CalculationResultDto> PerformCalculationAsync(CreateCalculationDto request)
    {
        try
        {
            // Verify session exists and is active
            var session = await _sessionRepository.GetByIdAsync(request.SessionId);
            if (session == null)
            {
                return new CalculationResultDto(Guid.Empty, 0, false, "Session not found");
            }

            if (!session.IsActive)
            {
                return new CalculationResultDto(Guid.Empty, 0, false, "Session is not active");
            }

            // Perform calculation
            double result = request.Operation switch
            {
                OperationType.Add => _calculator.Add((int)request.Operand1, (int)request.Operand2),
                OperationType.Subtract => _calculator.Subtract((int)request.Operand1, (int)request.Operand2),
                OperationType.Multiply => _calculator.Multiply((int)request.Operand1, (int)request.Operand2),
                OperationType.Divide => _calculator.Divide((int)request.Operand1, (int)request.Operand2),
                OperationType.Modulo => _calculator.Modulo((int)request.Operand1, (int)request.Operand2),
                OperationType.Power => _calculator.Power((int)request.Operand1, (int)request.Operand2),
                _ => throw new InvalidOperationException($"Unknown operation: {request.Operation}")
            };

            // Create and save calculation entity
            var calculation = new Calculation(
                request.SessionId,
                request.Operation,
                request.Operand1,
                request.Operand2,
                result);

            await _calculationRepository.AddAsync(calculation);

            // Add to session
            session.AddCalculation(calculation.Id);
            await _sessionRepository.UpdateAsync(session);

            return new CalculationResultDto(calculation.Id, result, true);
        }
        catch (DivideByZeroException ex)
        {
            return new CalculationResultDto(Guid.Empty, 0, false, ex.Message);
        }
        catch (ArgumentException ex)
        {
            return new CalculationResultDto(Guid.Empty, 0, false, ex.Message);
        }
    }

    public async Task<CalculationDto?> GetCalculationByIdAsync(Guid id)
    {
        var calculation = await _calculationRepository.GetByIdAsync(id);
        return calculation == null ? null : MapToDto(calculation);
    }

    public async Task<IEnumerable<CalculationDto>> GetSessionCalculationsAsync(Guid sessionId)
    {
        var calculations = await _calculationRepository.GetBySessionIdAsync(sessionId);
        return calculations.Select(MapToDto);
    }

    public async Task<IEnumerable<CalculationDto>> GetCalculationsByOperationAsync(OperationType operationType)
    {
        var calculations = await _calculationRepository.GetByOperationTypeAsync(operationType);
        return calculations.Select(MapToDto);
    }

    public async Task DeleteCalculationAsync(Guid id)
    {
        var calculation = await _calculationRepository.GetByIdAsync(id);
        if (calculation == null)
        {
            throw new InvalidOperationException($"Calculation with id '{id}' not found");
        }

        await _calculationRepository.DeleteAsync(id);
    }

    private static CalculationDto MapToDto(Calculation calculation)
    {
        return new CalculationDto(
            calculation.Id,
            calculation.SessionId,
            calculation.Operation,
            calculation.Operand1,
            calculation.Operand2,
            calculation.Result,
            calculation.Timestamp,
            calculation.IsValid);
    }
}
