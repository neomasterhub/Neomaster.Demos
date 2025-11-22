using Neomaster.Demos.ReadmeBuilder;

var readme = ReadmeBuilder.CreateBuilder()
  .CreateTestList("Archives", "Archives", "📦")
  .CreateTestList("LinqExpr", "LINQ", "🔗")
  .Build();

Console.WriteLine(readme);
