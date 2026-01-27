namespace Domain.Entities;

public class CalculationSession
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? ClosedAt { get; private set; }
    public bool IsActive { get; private set; }
    private readonly List<Guid> _calculationIds;
    public IReadOnlyCollection<Guid> CalculationIds => _calculationIds.AsReadOnly();

    public CalculationSession(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Session name cannot be empty", nameof(name));
        }

        Id = Guid.NewGuid();
        Name = name;
        CreatedAt = DateTime.UtcNow;
        IsActive = true;
        _calculationIds = new List<Guid>();
    }

    public void AddCalculation(Guid calculationId)
    {
        if (!IsActive)
        {
            throw new InvalidOperationException("Cannot add calculations to a closed session");
        }

        if (calculationId == Guid.Empty)
        {
            throw new ArgumentException("Calculation ID cannot be empty", nameof(calculationId));
        }

        if (_calculationIds.Contains(calculationId))
        {
            throw new InvalidOperationException($"Calculation {calculationId} already exists in this session");
        }

        _calculationIds.Add(calculationId);
    }

    public void CloseSession()
    {
        if (!IsActive)
        {
            throw new InvalidOperationException("Session is already closed");
        }

        IsActive = false;
        ClosedAt = DateTime.UtcNow;
    }

    public void ReopenSession()
    {
        if (IsActive)
        {
            throw new InvalidOperationException("Session is already active");
        }

        IsActive = true;
        ClosedAt = null;
    }

    public void Rename(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
        {
            throw new ArgumentException("New name cannot be empty", nameof(newName));
        }

        Name = newName;
    }

    public int GetCalculationCount()
    {
        return _calculationIds.Count;
    }
}
