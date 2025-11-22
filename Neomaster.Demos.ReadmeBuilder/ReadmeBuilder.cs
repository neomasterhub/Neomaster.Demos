using System.Text;
using Neomaster.Demos.Shared;

namespace Neomaster.Demos.ReadmeBuilder;

internal static class ReadmeBuilder
{
  private static readonly string _testsProjectName = "Neomaster.Demos.Tests";
  private static readonly int _localPathStartIndex = SolutionInfo.SolutionPath.Length + 1;

  public static void CreateTestList(string folder)
  {
    var dir = Path.Combine(
      SolutionInfo.SolutionPath,
      _testsProjectName,
      folder);

    foreach (var fi in new DirectoryInfo(dir).EnumerateFiles())
    {
      var items = new StringBuilder();
      var links = new StringBuilder();
      var linkPrefix = Guid.NewGuid().ToString();

      var i = 0;
      var n = 0;
      foreach (var line in File.ReadLines(fi.FullName))
      {
        n++;
        if (!line.StartsWith("  [Fact") && !line.StartsWith("  [Theory"))
        {
          continue;
        }

        i++;
        var name = line.Substring(23).Trim("\")]");
        var localPath = fi.FullName.Substring(_localPathStartIndex).Replace("\\", "/");
        var item = $"{i}. [{name}][{linkPrefix}-{i}]";
        var link = $"[{linkPrefix}-{i}]:{localPath}#L{n}";

        items.AppendLine(item);
        links.AppendLine(link);
      }
    }
  }
}
