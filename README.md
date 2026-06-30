# gh-report-validation

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

## Layout

- `CalculatorTests/` — a `Calculator` and passing [TUnit](https://github.com/thomhurst/TUnit) tests,
  plus one deliberately **slow** test to trigger slow-test notices.
- `StringUtilsTests/` — a `StringUtils` and tests, including one deliberately **failing** test to
  trigger a failure annotation and a non-empty "Failures" section in the job summary.
- `local-packages/` — the locally-built extension packaged as **NuGet packages**
  (`Microsoft.Testing.Extensions.GitHubActionsReport` + the `Microsoft.Testing.Platform` packages it
  was compiled against). `nuget.config` points at this folder so the extension is wired into the
  solution **at restore/build time** without needing the testfx repo.
- `.github/workflows/verify.yml` — builds the solution and runs each test project with
  `dotnet run --project`, so a maintainer can eyeball the Actions run and confirm the outputs render.

## Run locally

```bash
dotnet build gh-report-validation.slnx -c Release
# emits ::group::/::error/job-summary (GITHUB_* vars are set automatically only on a runner)
dotnet run --project StringUtilsTests/StringUtilsTests.csproj -c Release --no-build
```

On a GitHub Actions runner the `GITHUB_ACTIONS`, `GITHUB_WORKSPACE`, and `GITHUB_STEP_SUMMARY`
environment variables are set automatically, which is what activates the extension and routes the
job summary to the run page.
