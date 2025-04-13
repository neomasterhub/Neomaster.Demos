using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace Neomaster.Demos.Tests.Documents;

internal static class OpenXMLTools
{
  public const int EmusPerInch = 914400;
  public const int EmusPerCm = 360000;
  public const int EmusPerPt = 12700;
  public const int EmusPerPixel = 9525;

  public static long CmToEmu(double cm) => (long)(cm * EmusPerCm);
  public static long InchesToEmu(double inches) => (long)(inches * EmusPerInch);
  public static long PtToEmu(double pt) => (long)(pt * EmusPerPt);
  public static long PxToEmu(int px) => px * EmusPerPixel;

  public static double EmuToCm(long emu) => emu / (double)EmusPerCm;
  public static double EmuToInches(long emu) => emu / (double)EmusPerInch;
  public static double EmuToPt(long emu) => emu / (double)EmusPerPt;
  public static double EmuToPx(long emu) => emu / (double)EmusPerPixel;

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
