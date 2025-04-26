using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Xunit;
using FakeData = Faker;

namespace Neomaster.Demos.Tests.Documents;

public class InsertFilledTemplatesUnitDemos : OpenXMLUnitDemosBase
{
  [Fact]
  public void UserCard_Text_ToTableCell()
  {
    var insertTemplatePath = GetTemplatePath("text-002_cards.docx");
    var outputTemplatePath = GetTemplatePath("text-002_shell_user-card.docx");
    var outputPath = GetOutputPath(UserCard_Text_ToTableCell);

    File.Copy(outputTemplatePath, outputPath, true);

    using var insertDoc = WordprocessingDocument.Open(insertTemplatePath, true);
    var insertBody = insertDoc.MainDocumentPart.Document.Body;
    var insertPars = insertBody.Descendants<Paragraph>().ToList();

    var insideUserCard = false;
    for (var i = 0; i < insertPars.Count; i++)
    {
      var p = insertPars[i];
      var pTexts = p.Descendants<Text>().ToList();
      var pTextsString = string.Concat(pTexts.Select(t => t.Text));

      if (pTextsString.Contains("@user-card-text"))
      {
        insideUserCard = true;
        p.Remove();
        continue;
      }

      if (pTextsString.Contains("user-card-text@"))
      {
        insideUserCard = false;
        p.Remove();
        continue;
      }

      if (!insideUserCard)
      {
        p.Remove();
        continue;
      }

      foreach (var t in pTexts)
      {
        t.Text = t.Text
          .Replace("$ID", "001")
          .Replace("$email", FakeData.Internet.Email())
          .Replace("$first-name", FakeData.Name.First())
          .Replace("$last-name", FakeData.Name.Last());
      }
    }

    using var outputDoc = WordprocessingDocument.Open(outputPath, true);
    var outputBody = outputDoc.MainDocumentPart.Document.Body;
    var outputPar = outputBody
      .Descendants<Paragraph>()
      .First(p => string.Concat(p.Descendants<Text>().Select(t => t.Text)).Contains("@user-card@"));

    foreach (var ip in insertBody.Descendants<Paragraph>())
    {
      outputPar.Parent.InsertBefore(ip.CloneNode(true), outputPar);
    }

    outputPar.Remove();
    outputDoc.MainDocumentPart.Document.Save();
  }

  [Fact]
  public void UserCard_Table_ToTableCell()
  {
    var insertTemplatePath = GetTemplatePath("text-002_cards.docx");
    var outputTemplatePath = GetTemplatePath("text-002_shell_user-card.docx");
    var outputPath = GetOutputPath(UserCard_Table_ToTableCell);

    File.Copy(outputTemplatePath, outputPath, true);

    using var insertDoc = WordprocessingDocument.Open(insertTemplatePath, true);
    var insertBody = insertDoc.MainDocumentPart.Document.Body;
    var insertElements = insertBody.Elements<OpenXmlElement>().ToList();

    var insideUserCard = false;
    var cloneElements = new List<OpenXmlElement>();
    foreach (var e in insertElements)
    {
      var eText = string.Concat(e.Descendants<Text>().Select(t => t.Text));

      if (eText.Contains("@user-card-table"))
      {
        insideUserCard = true;
        continue;
      }

      if (eText.Contains("user-card-table@"))
      {
        insideUserCard = false;
        continue;
      }

      if (insideUserCard)
      {
        var clonedElement = e.CloneNode(true);

        foreach (var t in clonedElement.Descendants<Text>())
        {
          t.Text = t.Text
            .Replace("$ID", "001")
            .Replace("$email", FakeData.Internet.Email())
            .Replace("$first-name", FakeData.Name.First())
            .Replace("$last-name", FakeData.Name.Last());
        }

        cloneElements.Add(clonedElement);
      }
    }

    using var outputDoc = WordprocessingDocument.Open(outputPath, true);
    var outputBody = outputDoc.MainDocumentPart.Document.Body;

    var targetPar = outputBody
      .Descendants<Paragraph>()
      .First(p => p.InnerText.Contains("@user-card@"));
    var targetTableCell = targetPar
      .Ancestors<TableCell>()
      .First();

    foreach (var ce in cloneElements)
    {
      targetPar.Parent.InsertBefore(ce.CloneNode(true), targetPar);
    }

    targetPar.Remove();
    outputDoc.MainDocumentPart.Document.Save();
  }
}
