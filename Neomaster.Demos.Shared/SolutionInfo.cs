namespace Neomaster.Demos.Shared;

public static class SolutionInfo
{
  static SolutionInfo()
  {
    const string solutionName = "Neomaster.Demos";

    SolutionPath = Environment.CurrentDirectory.Substring(
      0, Environment.CurrentDirectory.IndexOf(solutionName) + solutionName.Length);

    ReadmePath = Path.Combine(SolutionPath, "readme.md");
  }

  public static string SolutionPath { get; }
  public static string ReadmePath { get; }
}
