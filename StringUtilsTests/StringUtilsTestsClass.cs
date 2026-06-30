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

    // Intentionally failing test to exercise failure annotations and the job summary.
    [Test]
    public async Task IsPalindrome_FailsOnPurpose()
        => await Assert.That(_utils.IsPalindrome("github")).IsTrue();
}
