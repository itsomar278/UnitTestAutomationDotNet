namespace Calculator;

public class CalculationStatistics
{
    public int TotalOperations { get; private set; }
    public int AddCount { get; private set; }
    public int SubtractCount { get; private set; }
    public int MultiplyCount { get; private set; }
    public int DivideCount { get; private set; }
    public int ModuloCount { get; private set; }
    public int PowerCount { get; private set; }
    public DateTime LastOperationTime { get; private set; }

    public void RecordAddition()
    {
        AddCount++;
        TotalOperations++;
        LastOperationTime = DateTime.UtcNow;
    }

    public void RecordSubtraction()
    {
        SubtractCount++;
        TotalOperations++;
        LastOperationTime = DateTime.UtcNow;
    }

    public void RecordMultiplication()
    {
        MultiplyCount++;
        TotalOperations++;
        LastOperationTime = DateTime.UtcNow;
    }

    public void RecordDivision()
    {
        DivideCount++;
        TotalOperations++;
        LastOperationTime = DateTime.UtcNow;
    }

    public void RecordModulo()
    {
        ModuloCount++;
        TotalOperations++;
        LastOperationTime = DateTime.UtcNow;
    }

    public void RecordPower()
    {
        PowerCount++;
        TotalOperations++;
        LastOperationTime = DateTime.UtcNow;
    }

    public void Reset()
    {
        TotalOperations = 0;
        AddCount = 0;
        SubtractCount = 0;
        MultiplyCount = 0;
        DivideCount = 0;
        ModuloCount = 0;
        PowerCount = 0;
        LastOperationTime = DateTime.MinValue;
    }

    public Dictionary<string, int> GetOperationBreakdown()
    {
        return new Dictionary<string, int>
        {
            { "Add", AddCount },
            { "Subtract", SubtractCount },
            { "Multiply", MultiplyCount },
            { "Divide", DivideCount },
            { "Modulo", ModuloCount },
            { "Power", PowerCount }
        };
    }
}
