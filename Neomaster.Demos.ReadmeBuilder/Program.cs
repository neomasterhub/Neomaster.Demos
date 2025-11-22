using Neomaster.Demos.ReadmeBuilder;

var readmeChapters = ReadmeBuilder.CreateBuilder()
  .CreateTestList("Archives", "Archives", "📦")
  .CreateTestList("LinqExpr", "LINQ", "🔗")
  .CreateTestList("Tasks", "Tasks", "📋")
  .CreateTestList("Threads", "Threads", "🔀")
  .Build();

Console.WriteLine(readmeChapters);
