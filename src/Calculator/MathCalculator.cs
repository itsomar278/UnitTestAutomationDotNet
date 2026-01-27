namespace Calculator;

public class MathCalculator
{
    public int Add(int a, int b)
    {
        return a + b;
    }

    public int Subtract(int a, int b)
    {
        return a - b;
    }

    public int Multiply(int a, int b)
    {
        return a * b;
    }

    public double Divide(int a, int b)
    {
        if (b == 0)
        {
            throw new DivideByZeroException("Cannot divide by zero");
        }
        return (double)a / b;
    }

    public int Modulo(int a, int b)
    {
        if (b == 0)
        {
            throw new DivideByZeroException("Cannot compute modulo by zero");
        }
        return a % b;
    }

    public double Power(int baseNumber, int exponent)
    {
        if (exponent < 0)
        {
            throw new ArgumentException("Exponent must be non-negative", nameof(exponent));
        }
        return Math.Pow(baseNumber, exponent);
    }
}
