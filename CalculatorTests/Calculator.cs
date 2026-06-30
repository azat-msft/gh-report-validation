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

    // INTENTIONAL BUG (for GitHub Actions annotation verification): this production method does not
    // guard against a zero total, so calling it with total == 0 throws DivideByZeroException from
    // *this* file. A test exercising that path fails, and the failure annotation should point here
    // (executable code), not at the test source.
    public int Percentage(int value, int total)
        => value * 100 / total;
}
