namespace Neomaster.Demos.Tests;

public class SolutionTests
{
  private static readonly string _solutionPath;
  private static readonly string _readmePath;

  static SolutionTests()
  {
    const string solutionName = "Neomaster.Demos";

    _solutionPath = Environment.CurrentDirectory.Substring(
      0, Environment.CurrentDirectory.IndexOf(solutionName) + solutionName.Length);

    _readmePath = Path.Combine(_solutionPath, "readme.md");
  }
}
