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

    var chapters = new Dictionary<string, string>();

    foreach (var fi in new DirectoryInfo(dir).EnumerateFiles())
    {
      string title = null;
      var items = new StringBuilder();
      var links = new StringBuilder();
      var localPath = fi.FullName.Substring(_localPathStartIndex).Replace('\\', '/');
      var linkPrefix = localPath.Replace('/', '_');

      var lineNumber = 0;
      var testNumber = 0;
      var lineEnumerator = File.ReadLines(fi.FullName).GetEnumerator();
      while (lineEnumerator.MoveNext())
      {
        var line = lineEnumerator.Current;

        lineNumber++;

        if (line.StartsWith("[Description"))
        {
          title = line.Substring(line.IndexOf('\"') + 1).TrimEnd("\")]").ToString();
          continue;
        }

        if (!line.StartsWith("  [Fact")
          && !line.StartsWith("  [Theory")
          && !line.StartsWith("  [ExternalDemo"))
        {
          continue;
        }

        testNumber++;

        string name = null;
        if (line.StartsWith("  [ExternalDemo"))
        {
          var nameLink = line.Substring(line.IndexOf('\"') + 1).TrimEnd("\")]").ToString().Split("\", \"");
          name = nameLink[0];
          localPath = nameLink[1];
        }
        else
        {
          name = line.Substring(line.IndexOf('\"') + 1).TrimEnd("\")]").ToString();
          localPath = fi.FullName.Substring(_localPathStartIndex).Replace('\\', '/');

          do
          {
            lineEnumerator.MoveNext();
            line = lineEnumerator.Current;
            lineNumber++;
          }
          while (!line.StartsWith("  public void") && !line.StartsWith("  public async"));

          localPath += $"#L{lineNumber}";
        }

        var item = $"{testNumber}. [{name}][{linkPrefix}-{testNumber}]";
        var link = $"[{linkPrefix}-{testNumber}]:{localPath}";

        items.AppendLine(item);
        links.AppendLine(link);
      }

      if (title == null)
      {
        throw new Exception($"[Description] not found in \"{fi.Name}\"");
      }

      var chapter =
        $"""
        &nbsp;&nbsp;&nbsp;&nbsp;**{title}**
        {items}
        {links}
        """;
      chapters.Add(title, chapter);
    }

    var chaptersString = string.Join(
      "\n\n",
      chapters.OrderBy(x => x.Key).Select(x => x.Value));

    var list =
      $"""
      <details>

      <summary><b>{headerEmoji} {header}</b></summary>

      <div>&nbsp;</div>

      {chaptersString}

      </details>
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
