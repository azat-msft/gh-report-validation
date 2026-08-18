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

    [Test]
    public async Task IsPalindrome_RejectsNonPalindrome()
        => await Assert.That(_utils.IsPalindrome("github")).IsFalse();
}
