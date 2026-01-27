namespace Calculator;

public class MathCalculator
{
    public CalculationStatistics Statistics { get; private set; }

    public MathCalculator()
    {
        Statistics = new CalculationStatistics();
    }

    public int Add(int a, int b)
    {
        Statistics.RecordAddition();
        return a + b;
    }

    public int Subtract(int a, int b)
    {
        Statistics.RecordSubtraction();
        return a - b;
    }

    public int Multiply(int a, int b)
    {
        Statistics.RecordMultiplication();
        return a * b;
    }

    public double Divide(int a, int b)
    {
        if (b == 0)
        {
            throw new DivideByZeroException("Cannot divide by zero");
        }
        Statistics.RecordDivision();
        return (double)a / b;
    }

    public int Modulo(int a, int b)
    {
        if (b == 0)
        {
            throw new DivideByZeroException("Cannot compute modulo by zero");
        }
        Statistics.RecordModulo();
        return a % b;
    }

    public double Power(int baseNumber, int exponent)
    {
        if (exponent < 0)
        {
            throw new ArgumentException("Exponent must be non-negative", nameof(exponent));
        }
        Statistics.RecordPower();
        return Math.Pow(baseNumber, exponent);
    }

    public void ResetStatistics()
    {
        Statistics.Reset();
    }
}
