using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Xunit;

namespace Neomaster.Demos.Tests.Documents.OpenXMLDocTables;

public class TableTemplatesUnitDemos
{
  [Fact]
  public void AddRows_ToFirstTable()
  {
    const string fileName = "table-001.docx";
    var templatePath = GetTemplatePath(fileName);
    var outputPath = Shared.GetOutputPath(fileName);

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
    var outputPath = Shared.GetOutputPath("table-001_email-hyperlink.docx");

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
}
