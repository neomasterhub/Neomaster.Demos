using Neomaster.Demos.ReadmeBuilder;
using Neomaster.Demos.Shared;

var readmeChapters = ReadmeBuilder.CreateBuilder()
  .CreateTestList("Archives", "Archives", "📦")
  .CreateTestList("LinqExpr", "LINQ", "🔗")
  .CreateTestList("Tasks", "Tasks", "📋")
  .CreateTestList("Threads", "Threads", "🔀")
  .Build();

var readmeTemplate = File.ReadAllText(Path.Combine(SolutionInfo.SolutionPath, "readme-template.md"));
var readme = readmeTemplate.Replace("{chapters}", readmeChapters);
File.WriteAllText(SolutionInfo.ReadmePath, readme);
