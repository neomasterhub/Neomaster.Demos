using DocumentFormat.OpenXml.Drawing.Wordprocessing;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Xunit;
using D = DocumentFormat.OpenXml.Drawing;
using DP = DocumentFormat.OpenXml.Drawing.Pictures;

namespace Neomaster.Demos.Tests.Documents;

public class ImagesUnitDemos : OpenXMLUnitDemosBase
{
  [Fact]
  public void Add_Watermark()
  {
    var templatePath = GetTemplatePath("text-001.docx");
    var imagePath = GetTemplatePath("bear-001_300x300.png");
    var outputPath = GetOutputPath(Add_Watermark);

    File.Copy(templatePath, outputPath, true);

    using var wordDoc = WordprocessingDocument.Open(outputPath, true);

    var mainPart = wordDoc.MainDocumentPart;
    var headerPart = mainPart.AddNewPart<HeaderPart>();
    var imagePart = headerPart.AddImagePart(ImagePartType.Png);
    var imageWidth = OpenXMLTools.PxToEmu(300);
    var imageHeight = OpenXMLTools.PxToEmu(300);

    using (var imageStream = File.OpenRead(imagePath))
    {
      imagePart.FeedData(imageStream);
    }

    var drawing = new Drawing(
      new Anchor(
        new SimplePosition { X = 0, Y = 0 },
        new HorizontalPosition(new HorizontalAlignment("center")) { RelativeFrom = HorizontalRelativePositionValues.Page },
        new VerticalPosition(new VerticalAlignment("center")) { RelativeFrom = VerticalRelativePositionValues.Page },
        new Extent { Cx = imageWidth, Cy = imageHeight },
        new EffectExtent { LeftEdge = 0, TopEdge = 0, RightEdge = 0, BottomEdge = 0 },
        new WrapNone(),
        new DocProperties { Id = 1, Name = "Watermark" },
        new D.NonVisualGraphicFrameDrawingProperties(new D.GraphicFrameLocks { NoChangeAspect = true }),
        new D.Graphic(
          new D.GraphicData(
            new DP.Picture(
              new DP.NonVisualPictureProperties(
                new DP.NonVisualDrawingProperties { Id = 0, Name = "Watermark" },
                new DP.NonVisualPictureDrawingProperties()),
              new DP.BlipFill(
                new D.Blip { Embed = headerPart.GetIdOfPart(imagePart), CompressionState = D.BlipCompressionValues.Print },
                new D.Stretch(new D.FillRectangle())),
              new DP.ShapeProperties(
                new D.Transform2D(
                  new D.Offset { X = 0, Y = 0 },
                  new D.Extents { Cx = imageWidth, Cy = imageHeight }),
                new D.PresetGeometry(new D.AdjustValueList()) { Preset = D.ShapeTypeValues.Rectangle })))
          { Uri = "http://schemas.openxmlformats.org/drawingml/2006/picture" }))
      {
        DistanceFromTop = 0,
        DistanceFromBottom = 0,
        DistanceFromLeft = 0,
        DistanceFromRight = 0,
        RelativeHeight = 1000, // z-index
        SimplePos = false, // relative position
        BehindDoc = false,
        AllowOverlap = false,
        Locked = true,
        LayoutInCell = true,
      });

    headerPart.Header = new Header(
      new Paragraph(
        new Run(drawing)));
    headerPart.Header.Save();

    var body = mainPart.Document.Body;
    var sp = body.Elements<SectionProperties>().FirstOrDefault();
    if (sp == null)
    {
      sp = new SectionProperties();
      body.Append(sp);
    }

    sp.RemoveAllChildren<HeaderReference>();
    sp.PrependChild(new HeaderReference
    {
      Type = HeaderFooterValues.Default,
      Id = mainPart.GetIdOfPart(headerPart),
    });

    mainPart.Document.Save();
  }
}
