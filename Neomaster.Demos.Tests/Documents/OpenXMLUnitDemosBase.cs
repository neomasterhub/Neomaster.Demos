namespace Neomaster.Demos.Tests.Documents;

public abstract class OpenXMLUnitDemosBase
{
  protected delegate void TestDelegate();

  protected static string GetTemplatePath(string fileName)
  {
    return Path.Combine("Documents", "Templates", fileName);
  }

  protected string GetOutputPath(TestDelegate test)
  {
    var type = GetType();
    var outputDir = Path.Combine(Shared.TestOutputDirectory, type.Namespace, type.Name);

    if (!Directory.Exists(outputDir))
    {
      Directory.CreateDirectory(outputDir);
    }

    return Path.Combine(outputDir, $"{test.Method.Name}.docx");
  }
}
