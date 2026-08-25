# gh-report-validation

> [!NOTE]
> **This is a throwaway demo repository, not a product.** It exists only so a reviewer can see the
> **`Microsoft.Testing.Extensions.GitHubActionsReport`** extension's output rendered by the real
> GitHub Actions UI while reviewing [microsoft/testfx#10633](https://github.com/microsoft/testfx/pull/10633).
> The tests here fail **on purpose** — a red run is the expected result, because failing tests are
> what a test report has to render. Nothing here is meant to be merged.

A small validation harness for the **`Microsoft.Testing.Extensions.GitHubActionsReport`**
[Microsoft.Testing.Platform](https://github.com/microsoft/testfx) extension
([microsoft/testfx#9142](https://github.com/microsoft/testfx/issues/9142)).

Its sole purpose is to **prove that every workflow command the extension emits is implemented
correctly and is actually picked up by the GitHub Actions UI**:

| Extension feature | Workflow command | Where it shows up in the UI |
|---|---|---|
| Per-assembly log groups | `::group::` / `::endgroup::` | Collapsible sections in the job log |
| Failure annotations | `::error file=,line=,col=,title=` | Annotations tab + the file's diff gutter |
| Slow-test notices | `::notice` | Annotations tab |
| Job summary | markdown → `$GITHUB_STEP_SUMMARY` | The run's **Summary** page |

## What the job summary demo shows

GitHub caps a job summary at 1 MB and **discards an oversized one in full** rather than trimming it,
so the extension gives up detail in stages as the shared file fills up. The 30 `Bulk*Tests` projects
exist to drive it through all three:

| Share of the 1 MB cap | What a test project gets |
|---|---|
| below 40% | Full section; each failure expanded into collapsible diagnostics |
| 40% – 60% | Full section; failures listed by name and duration, diagnostics dropped |
| above 60% | A single-line verdict, and a warning at the top of the summary saying so |

## Layout

- `CalculatorTests/` — a `Calculator` and passing [TUnit](https://github.com/thomhurst/TUnit) tests,
  plus one deliberately **slow** test to trigger slow-test notices.
- `StringUtilsTests/` — a `StringUtils` and tests, including one deliberately **failing** test to
  trigger a failure annotation and a non-empty "Failures" section in the job summary.
- `Bulk01Tests/` … `Bulk30Tests/` — near-identical projects that each contribute 25 failures with
  long messages and deep stack traces, so one run exercises every degradation stage above.
- `local-packages/` — the locally-built extension packaged as **NuGet packages**
  (`Microsoft.Testing.Extensions.GitHubActionsReport` + the `Microsoft.Testing.Platform` packages it
  was compiled against). `nuget.config` points at this folder so the extension is wired into the
  solution **at restore/build time** without needing the testfx repo.
- `.github/workflows/verify.yml` — builds the solution, runs each test project with
  `dotnet run --project`, and then asserts the rendered summary: that it stays under the cap, that
  every project is still named, and that the warning appears if and only if projects were condensed.
  It also uploads the rendered summary as the `job-summary` artifact so it can be inspected after the
  run.

## Run locally

```bash
dotnet build gh-report-validation.slnx -c Release
# emits ::group::/::error/job-summary (GITHUB_* vars are set automatically only on a runner)
dotnet run --project StringUtilsTests/StringUtilsTests.csproj -c Release --no-build
```

On a GitHub Actions runner the `GITHUB_ACTIONS`, `GITHUB_WORKSPACE`, and `GITHUB_STEP_SUMMARY`
environment variables are set automatically, which is what activates the extension and routes the
job summary to the run page.


## Run locally

```bash
dotnet build gh-report-validation.slnx -c Release
# emits ::group::/::error/job-summary (GITHUB_* vars are set automatically only on a runner)
dotnet run --project StringUtilsTests/StringUtilsTests.csproj -c Release --no-build
```

On a GitHub Actions runner the `GITHUB_ACTIONS`, `GITHUB_WORKSPACE`, and `GITHUB_STEP_SUMMARY`
environment variables are set automatically, which is what activates the extension and routes the
job summary to the run page.
