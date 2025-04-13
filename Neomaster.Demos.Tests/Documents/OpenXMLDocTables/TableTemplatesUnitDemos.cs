using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Xunit;

namespace Neomaster.Demos.Tests.Documents.OpenXMLDocTables;

public class TableTemplatesUnitDemos
{
  [Fact]
  public void AddRows_ToFirstTable()
  {
    var templatePath = GetTemplatePath("table-001.docx");
    var outputPath = GetOutputPath($"{nameof(AddRows_ToFirstTable)}.docx");

    File.Copy(templatePath, outputPath, true);

    using var wordDoc = WordprocessingDocument.Open(outputPath, true);

    var body = wordDoc.MainDocumentPart.Document.Body;
    var table = body.Elements<Table>().FirstOrDefault();
    var templateRow = table.Elements<TableRow>().ElementAt(1);

    for (var i = 1; i <= 10; i++)
    {
      var newRow = (TableRow)templateRow.CloneNode(true);

      foreach (var cell in newRow.Elements<TableCell>())
      {
        var text = cell.InnerText
          .Replace("{{ID}}", i.ToString())
          .Replace("{{Email}}", Faker.Internet.Email())
          .Replace("{{First Name}}", Faker.Name.First())
          .Replace("{{Last Name}}", Faker.Name.Last());

        cell.RemoveAllChildren<Paragraph>();
        cell.Append(new Paragraph(new Run(new Text(text))));
      }

      table.AppendChild(newRow);
    }

    table.RemoveChild(templateRow);

    wordDoc.MainDocumentPart.Document.Save();
  }

  [Fact]
  public void AddRows_ToFirstTable_EmailHyperlink()
  {
    var templatePath = GetTemplatePath("table-001.docx");
    var outputPath = GetOutputPath($"{nameof(AddRows_ToFirstTable_EmailHyperlink)}.docx");

    File.Copy(templatePath, outputPath, true);

    using var wordDoc = WordprocessingDocument.Open(outputPath, true);

    var body = wordDoc.MainDocumentPart.Document.Body;
    var table = body.Elements<Table>().FirstOrDefault();
    var templateRow = table.Elements<TableRow>().ElementAt(1);

    for (var i = 1; i <= 10; i++)
    {
      var newRow = (TableRow)templateRow.CloneNode(true);

      foreach (var cell in newRow.Elements<TableCell>())
      {
        var par = new Paragraph();
        var text = cell.InnerText;

        if (text.Contains("{{Email}}"))
        {
          par.Append(
            OpenXMLTools.CreateEmailHyperlink(
              wordDoc.MainDocumentPart,
              Faker.Internet.Email(),
              new Color { Val = "#0070C0" },
              new Underline { Val = UnderlineValues.Single }));
        }
        else
        {
          par.Append(
            new Run(
              new Text(
                text
                  .Replace("{{ID}}", i.ToString())
                  .Replace("{{Email}}", Faker.Internet.Email())
                  .Replace("{{First Name}}", Faker.Name.First())
                  .Replace("{{Last Name}}", Faker.Name.Last()))));
        }

        cell.RemoveAllChildren<Paragraph>();
        cell.Append(par);
      }

      table.AppendChild(newRow);
    }

    table.RemoveChild(templateRow);

    wordDoc.MainDocumentPart.Document.Save();
  }

  private static string GetTemplatePath(string fileName)
  {
    return Path.Combine("Documents", "OpenXMLDocTables", "Templates", fileName);
  }

  private static string GetOutputPath(string fileName)
  {
    var outputDir = Path.Combine(Shared.TestOutputDirectory, nameof(TableTemplatesUnitDemos));

    if (!Directory.Exists(outputDir))
    {
      Directory.CreateDirectory(outputDir);
    }

    return Path.Combine(outputDir, fileName);
  }
}
