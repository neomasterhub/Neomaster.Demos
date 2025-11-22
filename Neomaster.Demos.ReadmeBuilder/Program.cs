using Neomaster.Demos.ReadmeBuilder;

var readme = ReadmeBuilder.CreateBuilder()
  .CreateTestList("Archives")
  .Build();

Console.WriteLine("Done!");
