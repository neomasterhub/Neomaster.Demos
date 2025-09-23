using System.Text.RegularExpressions;
using Xunit;

namespace Neomaster.Demos.Tests;

public class SolutionTests(ITestOutputHelper output)
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

  [Fact]
  public void ReadmeShouldContainReferencesToTestMethods()
  {
    var testFileLineIndexes = File.ReadAllLines(_readmePath)
      .Where(line => Regex.IsMatch(line, @"#L[0-9]+$"))
      .Select(line =>
      {
        var parts = line.Split("#L");
        var test = new
        {
          FileName = parts[0].Substring(parts[0].LastIndexOf('/') + 1),
          MethodLineIndex = int.Parse(parts[1]),
        };

        return test;
      })
      .GroupBy(t => t.FileName, t => t.MethodLineIndex, (k, v) => new
      {
        FileName = k,
        MethodLineIndexes = v,
      });

    var unitDemoFileInfos = new DirectoryInfo(_solutionPath)
      .EnumerateFiles("*UnitDemos.cs", SearchOption.AllDirectories)
      .Where(fi => testFileLineIndexes.Any(l => l.FileName == fi.Name));

    var foundDemos = new List<string>();

    foreach (var udFi in unitDemoFileInfos)
    {
      var udLines = File.ReadAllLines(udFi.FullName);
      var testLineIndexes = testFileLineIndexes
        .Single(x => x.FileName == udFi.Name).MethodLineIndexes;

      foreach (var i in testLineIndexes)
      {
        output.WriteLine($"Checking {udFi.Name}#{i}...");

        var udLine = udLines[i - 1].TrimStart();

        output.WriteLine($"         Found: {udLine}");

        Assert.Matches(@"public (void|async Task)", udLine);

        var foundDemo = $"{udFi.Name}#{i}:{udLine}";
        Assert.DoesNotContain(foundDemo, foundDemos);

        foundDemos.Add(foundDemo);
      }
    }
  }

  [Fact]
  public void ReadmeShouldOrderedLists()
  {
    var prevIndex = 0;
    string prevRefPrefix = null;

    foreach (var item in File
      .ReadAllLines(_readmePath)
      .Where(line => Regex.IsMatch(line, @"^[0-9]+\.")))
    {
      var index = int.Parse(item.Split('.')[0]);
      var itemEnd = $"-{index}]";
      var refPrefix = item.Split("][")[1].Replace(itemEnd, string.Empty);

      Assert.EndsWith(itemEnd, item);

      if (prevRefPrefix == refPrefix)
      {
        Assert.Equal(prevIndex + 1, index);
        prevIndex = index;
      }
      else
      {
        Assert.Equal(1, index);
        prevIndex = 1;
        prevRefPrefix = refPrefix;
      }
    }
  }
}
