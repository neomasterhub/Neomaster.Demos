using System.Text;
using Neomaster.Demos.Shared;

namespace Neomaster.Demos.ReadmeBuilder;

internal class CppReadmeBuilder
{
  private static readonly string _cppProjectName = "Neomaster.Demos.Cpp";
  private static readonly int _localPathStartIndex = SolutionInfo.SolutionPath.Length + 1;

  private readonly Dictionary<string, (string items, string links)> _chapters = [];

  public CppReadmeBuilder CreateTestList(
    string title,
    string headerFileName,
    string sourceFileName = null)
  {
    sourceFileName ??= Path.ChangeExtension(headerFileName, ".cpp");

    var headerFilePath = Path.Combine(SolutionInfo.SolutionPath, _cppProjectName, headerFileName);
    var sourceFilePath = Path.Combine(SolutionInfo.SolutionPath, _cppProjectName, sourceFileName);

    var headerFileLines = File.ReadAllLines(headerFilePath);
    var sourceFileLines = File.ReadAllLines(sourceFilePath);

    var items = new StringBuilder();
    var links = new StringBuilder();

    var nextIsMethodSignature = false;
    var sourceFileTestIndex = 0;
    string testName = null;
    var testNumber = 0;
    foreach (var hLine in headerFileLines)
    {
      if (hLine.Contains("DN: "))
      {
        testName = hLine.Split("DN: ").Last();
        continue;
      }

      if (testName != null && hLine.Contains("</summary>"))
      {
        nextIsMethodSignature = true;
        continue;
      }

      if (nextIsMethodSignature)
      {
        nextIsMethodSignature = false;
        var methodName = hLine.Split(' ').Last()[..^1];

        for (var i = sourceFileTestIndex; i < sourceFileLines.Length; i++)
        {
          if (sourceFileLines[i].Contains(methodName))
          {
            sourceFileTestIndex = i;
            break;
          }
        }

        var localPath = sourceFilePath[_localPathStartIndex..].Replace('\\', '/');
        var linkPrefix = localPath.Replace('/', '_');

        testNumber++;
        var item = $"{testNumber}. [{testName}][{linkPrefix}-{testNumber}]";
        var link = $"[{linkPrefix}-{testNumber}]:{localPath}#{sourceFileTestIndex}";

        items.AppendLine(item);
        links.AppendLine(link);

        testName = null;
      }
    }

    _chapters.Add(title, (items.ToString(), links.ToString()));

    return this;
  }
}
