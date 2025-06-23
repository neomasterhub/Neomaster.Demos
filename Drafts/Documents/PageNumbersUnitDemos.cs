using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Xunit;

namespace Neomaster.Demos.Tests.Documents;

public class PageNumbersUnitDemos : OpenXMLUnitDemosBase
{
  [Fact]
  public void Add()
  {
    var templatePath = GetTemplatePath("text-001.docx");
    var outputPath = GetOutputPath(Add);

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
    var templatePath = GetTemplatePath("text-001.docx");
    var outputPath = GetOutputPath(Add_OfTotal);

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

  [Theory]
  [InlineData("text-001.docx", false)]
  [InlineData("text-001_page-numbers.docx", true)]
  public void Has(string fileName, bool hasPageNumbers)
  {
    using var wordDoc = WordprocessingDocument.Open(GetTemplatePath(fileName), false);

    var mainPart = wordDoc.MainDocumentPart;
    var foundPageNumbers = wordDoc.MainDocumentPart.Document
      .Descendants<FooterReference>()
      .Select(fr => ((FooterPart)mainPart.GetPartById(fr.Id)).Footer)
      .Any(f => f != null && f.Descendants().Any(e =>
          e.LocalName == "instrText"
          && e.InnerText.Contains("PAGE")));

    Assert.Equal(hasPageNumbers, foundPageNumbers);
  }
}
