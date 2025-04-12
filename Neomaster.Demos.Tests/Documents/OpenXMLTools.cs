using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace Neomaster.Demos.Tests.Documents;

internal static class OpenXMLTools
{
  public static Hyperlink CreateEmailHyperlink(
    MainDocumentPart mainPart,
    string email,
    params OpenXmlLeafElement[] properties)
  {
    var relationship = mainPart.AddHyperlinkRelationship(
      new Uri($"mailto:{email}", UriKind.Absolute),
      true);

    var hyperlink = new Hyperlink(new Run(new RunProperties(properties), new Text(email)))
    {
      History = OnOffValue.FromBoolean(true),
      Id = relationship.Id,
    };

    return hyperlink;
  }
}
