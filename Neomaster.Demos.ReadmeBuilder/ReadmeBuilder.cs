using System.Text;
using Neomaster.Demos.Shared;

namespace Neomaster.Demos.ReadmeBuilder;

internal class ReadmeBuilder
{
  private static readonly string _testsProjectName = "Neomaster.Demos.Tests";
  private static readonly int _localPathStartIndex = SolutionInfo.SolutionPath.Length + 1;

  private readonly Dictionary<string, string> _testLists = [];

  private ReadmeBuilder()
  {
  }

  public static ReadmeBuilder CreateBuilder() => new();

  public ReadmeBuilder CreateTestList(string folder, string header, string headerEmoji)
  {
    var dir = Path.Combine(
      SolutionInfo.SolutionPath,
      _testsProjectName,
      folder);

    var chapters = new StringBuilder();

    foreach (var fi in new DirectoryInfo(dir).EnumerateFiles())
    {
      string title = null;
      var items = new StringBuilder();
      var links = new StringBuilder();
      var localPath = fi.FullName.Substring(_localPathStartIndex).Replace('\\', '/');
      var linkPrefix = localPath.Replace('/', '_');

      var lineNumber = 0;
      var testNumber = 0;
      foreach (var line in File.ReadLines(fi.FullName))
      {
        lineNumber++;

        if (line.StartsWith("[Description"))
        {
          title = line.Substring(line.IndexOf('\"') + 1).TrimEnd("\")]").ToString();
          continue;
        }

        if (!line.StartsWith("  [Fact") && !line.StartsWith("  [Theory"))
        {
          continue;
        }

        testNumber++;
        var name = line.Substring(line.IndexOf('\"') + 1).TrimEnd("\")]");
        var item = $"{testNumber}. [{name}][{linkPrefix}-{testNumber}]";
        var link = $"[{linkPrefix}-{testNumber}]:{localPath}#L{lineNumber}";

        items.AppendLine(item);
        links.AppendLine(link);
      }

      if (title == null)
      {
        throw new Exception($"[Description] not found in \"{fi.Name}\"");
      }

      chapters.AppendLine(
        $"""
        <details>
        <summary>{title}</summary>

        {items}
        {links}
        </details>
        """);
    }

    var list =
      $"""
      ### {headerEmoji} {header}

      {chapters}
      """;

    _testLists.Add(header, list);

    return this;
  }

  public string Build()
  {
    var text = string.Join(
      "\n\n",
      _testLists.OrderBy(x => x.Key).Select(x => x.Value));

    return text;
  }
}
