using Neomaster.Demos.ReadmeBuilder;
using Neomaster.Demos.Shared;

var readmeChaptersDotnet = ReadmeBuilder.CreateBuilder()
  .CreateTestList("Archives", "Archives", "📦")
  .CreateTestList("Threads", "Threads", "🔀")
  .CreateTestList("Tasks", "Tasks", "📋")
  .CreateTestList("LinqExpr", "LINQ", "🔗")
  .Build();

var readmeChaptersCpp = new CppReadmeBuilder()
  .CreateTestList("🧱 Fundamentals", "Fundamentals.h")
  .Build();

var readmeTemplate = File.ReadAllText(Path.Combine(SolutionInfo.SolutionPath, "readme-template.md"));

var readme = readmeTemplate
  .Replace("{chapters .net}", readmeChaptersDotnet)
  .Replace("{chapters c++}", readmeChaptersCpp);

File.WriteAllText(SolutionInfo.ReadmePath, readme);
