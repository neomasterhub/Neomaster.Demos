using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Xunit;

namespace Neomaster.Demos.Tests.Documents.OpenXMLDocFooters;

public class PageNumbersUnitDemos
{
  [Fact]
  public void Add()
  {
    const string fileName = "text-001.docx";
    var templatePath = GetTemplatePath(fileName);
    var outputPath = Shared.GetOutputPath(fileName);

    File.Copy(templatePath, outputPath, true);

    using var wordDoc = WordprocessingDocument.Open(outputPath, true);

    var mainPart = wordDoc.MainDocumentPart;
    var footerPart = mainPart.AddNewPart<FooterPart>();
    var footerPartId = mainPart.GetIdOfPart(footerPart);

    footerPart.Footer = new Footer(
      new Paragraph(
        new ParagraphProperties(
          new Justification() { Val = JustificationValues.Center }),
        new Run(
          new FieldChar() { FieldCharType = FieldCharValues.Begin },
          new FieldCode("PAGE") { Space = SpaceProcessingModeValues.Preserve },
          new FieldChar() { FieldCharType = FieldCharValues.Separate },
          new FieldChar() { FieldCharType = FieldCharValues.End })));
    footerPart.Footer.Save();

    var sp = mainPart.Document.Body.Elements<SectionProperties>().LastOrDefault();
    if (sp == null)
    {
      sp = new SectionProperties();
      mainPart.Document.Body.Append(sp);
    }

    sp.Append(
      new FooterReference()
      {
        Id = footerPartId,
        Type = HeaderFooterValues.Default,
      });

    wordDoc.Save();
  }

  [Fact]
  public void Add_OfTotal()
  {
    const string fileName = "text-001.docx";
    var templatePath = GetTemplatePath(fileName);
    var outputPath = Shared.GetOutputPath(fileName);

    File.Copy(templatePath, outputPath, true);

    using var wordDoc = WordprocessingDocument.Open(outputPath, true);

    var mainPart = wordDoc.MainDocumentPart;
    var footerPart = mainPart.AddNewPart<FooterPart>();
    var footerPartId = mainPart.GetIdOfPart(footerPart);

    footerPart.Footer = new Footer(
      new Paragraph(
        new ParagraphProperties(
          new Justification() { Val = JustificationValues.Center }),
        new Run(
          new FieldChar() { FieldCharType = FieldCharValues.Begin },
          new FieldCode("PAGE") { Space = SpaceProcessingModeValues.Preserve },
          new FieldChar() { FieldCharType = FieldCharValues.Separate },
          new FieldChar() { FieldCharType = FieldCharValues.End },
          new Text(" / "),
          new FieldChar() { FieldCharType = FieldCharValues.Begin },
          new FieldCode("NUMPAGES") { Space = SpaceProcessingModeValues.Preserve },
          new FieldChar() { FieldCharType = FieldCharValues.Separate },
          new FieldChar() { FieldCharType = FieldCharValues.End })));
    footerPart.Footer.Save();

    var sp = mainPart.Document.Body.Elements<SectionProperties>().LastOrDefault();
    if (sp == null)
    {
      sp = new SectionProperties();
      mainPart.Document.Body.Append(sp);
    }

    sp.Append(
      new FooterReference()
      {
        Id = footerPartId,
        Type = HeaderFooterValues.Default,
      });

    wordDoc.Save();
  }

  private static string GetTemplatePath(string fileName)
  {
    return Path.Combine("Documents", "OpenXMLDocFooters", "Templates", fileName);
  }
}
