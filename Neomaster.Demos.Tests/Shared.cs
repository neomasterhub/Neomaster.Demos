global using Process = System.Diagnostics.Process;
global using ProcessStartInfo = System.Diagnostics.ProcessStartInfo;
global using Stopwatch = System.Diagnostics.Stopwatch;

namespace Neomaster.Demos.Tests;

public static class Shared
{
  private const string _testOutputFolder = "___neomaster.demos-tests";

  public static readonly string TestOutputDirectory;

  static Shared()
  {
    TestOutputDirectory = Path.Combine(
      Environment.GetEnvironmentVariable("temp"),
      _testOutputFolder);

    if (!Directory.Exists(TestOutputDirectory))
    {
      Directory.CreateDirectory(TestOutputDirectory);
    }
  }

  public static string GetOutputPath(string fileName)
  {
    return Path.Combine(TestOutputDirectory, fileName);
  }
}
