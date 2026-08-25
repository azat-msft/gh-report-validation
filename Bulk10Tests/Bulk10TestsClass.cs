namespace Bulk10Tests;

/// <summary>
/// One of 30 near-identical test projects used to validate that the GitHub Actions job summary stays within
/// GitHub's 1 MiB cap when many test projects append to the same GITHUB_STEP_SUMMARY file.
/// </summary>
/// <remarks>
/// Each project contributes 25 failures carrying a multi-line message and a deep stack trace, which is far
/// more expanded detail than the shared budget can grant to 30 projects at once. The summary must therefore
/// truncate explicitly and say so, rather than silently dropping content or overflowing the cap.
/// </remarks>
public sealed class Bulk10TestsClass
{
    public static IEnumerable<int> Cases() => Enumerable.Range(1, 25);

    [Test]
    [MethodDataSource(nameof(Cases))]
    public Task FailsWithDetailedDiagnostics(int caseNumber)
        => ThrowFromDeepStack(caseNumber, depth: 25);

    private static Task ThrowFromDeepStack(int caseNumber, int depth)
    {
        if (depth > 0)
        {
            return ThrowFromDeepStack(caseNumber, depth - 1);
        }

        string detail = string.Join(
            "\n",
            Enumerable.Range(0, 40).Select(line => $"  line {line:0000}: expected 'alpha-{caseNumber}' but found 'beta-{caseNumber}'"));

        throw new InvalidOperationException($"Bulk10Tests case {caseNumber} failed:\n{detail}");
    }
}