namespace StringUtilsTests;

/// <summary>
/// Drives the job summary with a very large <em>number</em> of failures whose individual diagnostics are
/// tiny, which is the opposite pressure from <c>TruncatedFailuresTestsClass</c> (few failures, enormous
/// diagnostics).
/// </summary>
/// <remarks>
/// Every case fails with a single short line, so no individual value is anywhere near the per-message or
/// per-stack-trace clip and the per-section detail budget is never exhausted. The only bound that engages is
/// the 20-failure list cap, so the reporter's section stays small and flat no matter how far the failure
/// count grows — the summary reports "Showing the first 20 of 5000 failed tests" and stops there.
/// <para>
/// Measured on this asset: 600 failures produce a 17,754-byte summary and 5,000 failures produce a
/// 17,809-byte one. The 55-byte delta is just the wider count rendered in the text, which is the point —
/// failure count alone cannot grow the summary, so it cannot push a run past GitHub's 1 MiB limit.
/// </para>
/// </remarks>
public sealed class ManyFailuresTestsClass
{
    public static IEnumerable<int> Cases() => Enumerable.Range(1, 5000);

    [Test]
    [MethodDataSource(nameof(Cases))]
    public Task FailsWithAShortMessage(int caseNumber)
        => throw new InvalidOperationException($"Case {caseNumber} failed: expected 'alpha' but found 'beta'.");
}
