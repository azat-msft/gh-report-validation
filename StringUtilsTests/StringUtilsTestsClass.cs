namespace StringUtilsTests;

public sealed class StringUtilsTestsClass
{
    private readonly StringUtils _utils = new();

    [Test]
    public async Task Reverse_ReversesString()
        => await Assert.That(_utils.Reverse("abc")).IsEqualTo("cba");

    [Test]
    public async Task IsPalindrome_DetectsPalindrome()
        => await Assert.That(_utils.IsPalindrome("level")).IsTrue();

    [Test]
    public async Task CountWords_CountsWords()
        => await Assert.That(_utils.CountWords("hello world foo")).IsEqualTo(3);

    // Intentionally failing tests to exercise the collapsible failure sections in the job summary.
    // Their messages and stack traces are short, so every section renders in full with no truncation.
    [Test]
    public async Task IsPalindrome_FailsOnPurpose()
        => await Assert.That(_utils.IsPalindrome("github")).IsTrue();

    [Test]
    public async Task Reverse_FailsOnPurpose()
        => await Assert.That(_utils.Reverse("abc")).IsEqualTo("abc");

    [Test]
    public async Task CountWords_FailsOnPurpose()
        => await Assert.That(_utils.CountWords("hello world foo")).IsEqualTo(4);

    [Test]
    public Task Throws_WithShortMessage()
        => throw new InvalidOperationException("Expected 42 but got 41.");
}
