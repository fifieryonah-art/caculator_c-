using Calculator.Core;

namespace Calculator.Tests;

public class ArithmeticTests
{
    [Theory]
    [InlineData(2, 3, 5)]
    [InlineData(-1, 1, 0)]
    [InlineData(0.5, 0.25, 0.75)]
    public void Add_ReturnsSum(double a, double b, double expected)
    {
        Assert.Equal(expected, Arithmetic.Add(a, b), 6);
    }

    [Theory]
    [InlineData(5, 3, 2)]
    [InlineData(3, 5, -2)]
    public void Subtract_ReturnsDifference(double a, double b, double expected)
    {
        Assert.Equal(expected, Arithmetic.Subtract(a, b), 6);
    }

    [Theory]
    [InlineData(4, 3, 12)]
    [InlineData(-2, 3, -6)]
    [InlineData(5, 0, 0)]
    public void Multiply_ReturnsProduct(double a, double b, double expected)
    {
        Assert.Equal(expected, Arithmetic.Multiply(a, b), 6);
    }

    [Fact]
    public void Divide_ReturnsQuotient()
    {
        Assert.Equal(2.5, Arithmetic.Divide(5, 2), 6);
    }

    [Fact]
    public void Divide_ByZero_ThrowsDivideByZeroException()
    {
        Assert.Throws<DivideByZeroException>(() => Arithmetic.Divide(5, 0));
    }
}