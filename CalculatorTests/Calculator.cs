namespace CalculatorTests;

/// <summary>
/// A tiny calculator used to exercise the GitHub Actions report extension.
/// </summary>
public sealed class Calculator
{
    public int Add(int a, int b) => a + b;

    public int Subtract(int a, int b) => a - b;

    public int Multiply(int a, int b) => a * b;

    public int Divide(int a, int b) => b == 0
        ? throw new DivideByZeroException("Cannot divide by zero.")
        : a / b;
}
