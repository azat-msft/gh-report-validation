namespace CalculatorTests;

public sealed class CalculatorTestsClass
{
    private readonly Calculator _calculator = new();

    [Test]
    public async Task Add_ReturnsSum()
        => await Assert.That(_calculator.Add(2, 3)).IsEqualTo(5);

    [Test]
    public async Task Subtract_ReturnsDifference()
        => await Assert.That(_calculator.Subtract(10, 4)).IsEqualTo(6);

    [Test]
    public async Task Multiply_ReturnsProduct()
        => await Assert.That(_calculator.Multiply(6, 7)).IsEqualTo(42);

    [Test]
    public async Task Divide_ReturnsQuotient()
        => await Assert.That(_calculator.Divide(20, 5)).IsEqualTo(4);

    [Test]
    public async Task Divide_ByZero_Throws()
        => await Assert.That(() => _calculator.Divide(1, 0)).Throws<DivideByZeroException>();

    // Deliberately slow test to exercise GitHub Actions slow-test notices (with a low threshold).
    [Test]
    public async Task Add_SlowOnPurpose()
    {
        await Task.Delay(TimeSpan.FromSeconds(3));
        await Assert.That(_calculator.Add(1, 1)).IsEqualTo(2);
    }

    // Passing test for the new production method.
    [Test]
    public async Task Percentage_ComputesRatio()
        => await Assert.That(_calculator.Percentage(25, 100)).IsEqualTo(25);

    // Failing test caused by a bug in PRODUCTION code (Calculator.Percentage divides by zero).
    // The failure annotation should point at Calculator.cs, not at this test file.
    [Test]
    public async Task Percentage_OfZeroTotal_ReturnsZero()
        => await Assert.That(_calculator.Percentage(0, 0)).IsEqualTo(0);
}
