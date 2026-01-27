using Domain.ValueObjects;

namespace Domain.Entities;

public class Calculation
{
    public Guid Id { get; private set; }
    public Guid SessionId { get; private set; }
    public OperationType Operation { get; private set; }
    public double Operand1 { get; private set; }
    public double Operand2 { get; private set; }
    public double Result { get; private set; }
    public DateTime Timestamp { get; private set; }
    public bool IsValid { get; private set; }

    public Calculation(Guid sessionId, OperationType operation, double operand1, double operand2, double result)
    {
        if (sessionId == Guid.Empty)
        {
            throw new ArgumentException("Session ID cannot be empty", nameof(sessionId));
        }

        Id = Guid.NewGuid();
        SessionId = sessionId;
        Operation = operation;
        Operand1 = operand1;
        Operand2 = operand2;
        Result = result;
        Timestamp = DateTime.UtcNow;
        IsValid = true;
    }

    public void MarkAsInvalid()
    {
        if (!IsValid)
        {
            throw new InvalidOperationException("Calculation is already marked as invalid");
        }
        IsValid = false;
    }

    public void UpdateResult(double newResult)
    {
        if (!IsValid)
        {
            throw new InvalidOperationException("Cannot update result of an invalid calculation");
        }
        Result = newResult;
    }
}
