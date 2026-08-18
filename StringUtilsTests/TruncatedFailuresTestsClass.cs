namespace StringUtilsTests;

/// <summary>
/// Drives the truncation paths of the GitHub Actions job summary's Failures section.
/// </summary>
/// <remarks>
/// 30 failing cases exceed the reporter's 20-failure cap, so the summary must state
/// "Showing the first 20 of 30 failed tests". Each failure also carries a message far larger than the
/// 2,000-character per-message budget and a deep stack trace, so individual values are clipped with a
/// "[... truncated]" marker and the per-section budget runs out partway down the list — the remaining
/// listed failures then degrade to compact lines and the summary reports how many had their details
/// omitted.
/// </remarks>
public sealed class TruncatedFailuresTestsClass
{
    private const int OversizedMessageLength = 6000;

    [Test]
    [Arguments(1)]
    [Arguments(2)]
    [Arguments(3)]
    [Arguments(4)]
    [Arguments(5)]
    [Arguments(6)]
    [Arguments(7)]
    [Arguments(8)]
    [Arguments(9)]
    [Arguments(10)]
    [Arguments(11)]
    [Arguments(12)]
    [Arguments(13)]
    [Arguments(14)]
    [Arguments(15)]
    [Arguments(16)]
    [Arguments(17)]
    [Arguments(18)]
    [Arguments(19)]
    [Arguments(20)]
    [Arguments(21)]
    [Arguments(22)]
    [Arguments(23)]
    [Arguments(24)]
    [Arguments(25)]
    [Arguments(26)]
    [Arguments(27)]
    [Arguments(28)]
    [Arguments(29)]
    [Arguments(30)]
    public Task FailsWithAnOversizedMessage(int caseNumber)
        => ThrowFromDeepStack(caseNumber, depth: 40);

    /// <summary>
    /// Recurses and throws at the bottom so the exception carries a genuinely deep stack trace, exercising
    /// the stack-trace clip and the per-section budget in addition to the message clip.
    /// </summary>
    private static Task ThrowFromDeepStack(int caseNumber, int depth)
    {
        if (depth > 0)
        {
            return ThrowFromDeepStack(caseNumber, depth - 1);
        }

        string detail = string.Join(
            "\n",
            Enumerable.Range(0, OversizedMessageLength / 60)
                .Select(line => $"  line {line:0000}: expected 'alpha-{caseNumber}' but found 'beta-{caseNumber}'"));

        throw new InvalidOperationException($"Case {caseNumber} failed with an oversized diff:\n{detail}");
    }
}
