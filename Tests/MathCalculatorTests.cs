using Calculator;
using System;
using Xunit;

namespace CalculatorTests
{
    public class MathCalculatorTests
    {
        [Fact]
        public void Add_PositiveNumbers_ReturnsSum()
        {
            // Arrange
            var calculator = new MathCalculator();
            int a = 5, b = 3;

            // Act
            var result = calculator.Add(a, b);

            // Assert
            Assert.Equal(8, result);
        }

        [Fact]
        public void Subtract_PositiveNumbers_ReturnsDifference()
        {
            // Arrange
            var calculator = new MathCalculator();
            int a = 5, b = 3;

            // Act
            var result = calculator.Subtract(a, b);

            // Assert
            Assert.Equal(2, result);
        }

        [Fact]
        public void Multiply_PositiveNumbers_ReturnsProduct()
        {
            // Arrange
            var calculator = new MathCalculator();
            int a = 5, b = 3;

            // Act
            var result = calculator.Multiply(a, b);

            // Assert
            Assert.Equal(15, result);
        }

        [Fact]
        public void Divide_PositiveNumbers_ReturnsQuotient()
        {
            // Arrange
            var calculator = new MathCalculator();
            int a = 6, b = 3;

            // Act
            var result = calculator.Divide(a, b);

            // Assert
            Assert.Equal(2.0, result);
        }

        [Fact]
        public void Divide_ByZero_ThrowsDivideByZeroException()
        {
            // Arrange
            var calculator = new MathCalculator();

            // Act & Assert
            Assert.Throws<DivideByZeroException>(() => calculator.Divide(5, 0));
        }

        [Fact]
        public void Modulo_PositiveNumbers_ReturnsModulo()
        {
            // Arrange
            var calculator = new MathCalculator();
            int a = 5, b = 3;

            // Act
            var result = calculator.Modulo(a, b);

            // Assert
            Assert.Equal(2, result);
        }

        [Fact]
        public void Modulo_ByZero_ThrowsDivideByZeroException()
        {
            // Arrange
            var calculator = new MathCalculator();

            // Act & Assert
            Assert.Throws<DivideByZeroException>(() => calculator.Modulo(5, 0));
        }

        [Fact]
        public void Power_PositiveExponent_ReturnsPower()
        {
            // Arrange
            var calculator = new MathCalculator();
            int baseNumber = 2, exponent = 3;

            // Act
            var result = calculator.Power(baseNumber, exponent);

            // Assert
            Assert.Equal(8.0, result);
        }

        [Fact]
        public void Power_NegativeExponent_ThrowsArgumentException()
        {
            // Arrange
            var calculator = new MathCalculator();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => calculator.Power(2, -1));
        }
    }
}
