using System.ComponentModel;
using System.IO.Compression;
using System.Text;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Xunit;

namespace Neomaster.Demos.Tests.Archives;

[Description("Zip")]
public class ArchivesZipUnitDemos()
{
  private delegate void TestDelegate();

  [Fact(DisplayName = "Create 1 root file")]
  public void Create_1_RootFile()
  {
    using var fs = new FileStream(GetOutputZipArchivePath(Create_1_RootFile), FileMode.Create);
    using var zip = new ZipArchive(fs, ZipArchiveMode.Create);
    using var ms = new MemoryStream(Encoding.UTF8.GetBytes("привет"));

    var zipEntry = zip.CreateEntry("privet.txt");
    using var ze = zipEntry.Open();
    ms.CopyTo(ze);
  }

  [Fact(DisplayName = "Create N root files")]
  public void Create_N_RootFiles()
  {
    using var fs = new FileStream(GetOutputZipArchivePath(Create_N_RootFiles), FileMode.Create);
    using var zip = new ZipArchive(fs, ZipArchiveMode.Create);

    for (var i = 1; i <= 3; i++)
    {
      using var ms = new MemoryStream(Encoding.UTF8.GetBytes($"привет {i}"));

      var zipEntry = zip.CreateEntry($"privet_{i}.txt");
      using var ze = zipEntry.Open();
      ms.CopyTo(ze);
    }
  }

  [Fact(DisplayName = "Create N folder files")]
  public void Create_N_FolderFiles()
  {
    using var fs = new FileStream(GetOutputZipArchivePath(Create_N_FolderFiles), FileMode.Create);
    using var zip = new ZipArchive(fs, ZipArchiveMode.Create);

    for (var i = 1; i <= 3; i++)
    {
      using var ms = new MemoryStream(Encoding.UTF8.GetBytes($"привет {i}"));

      var zipEntry = zip.CreateEntry($"privet_{i}/privet_{i}.txt");
      using var ze = zipEntry.Open();
      ms.CopyTo(ze);
    }
  }

  [Fact(DisplayName = "Create Word doc")]
  public void Create_WordDoc()
  {
    using var fs = new FileStream(GetOutputZipArchivePath(Create_1_RootFile), FileMode.Create);
    using var zip = new ZipArchive(fs, ZipArchiveMode.Create);
    using var ms = new MemoryStream();

    using (var wordDoc = WordprocessingDocument.Create(ms, WordprocessingDocumentType.Document, true))
    {
      var mainPart = wordDoc.AddMainDocumentPart();
      mainPart.Document = new Document(
        new Body(
          new Paragraph(
            new Run(
              new Text("привет")))));
      mainPart.Document.Save();
    }

    var zipEntry = zip.CreateEntry("privet.docx");
    using var ze = zipEntry.Open();
    ms.Seek(0, SeekOrigin.Begin);
    ms.CopyTo(ze);
  }

  private string GetOutputZipArchivePath(TestDelegate test)
  {
    var type = GetType();
    var outputDir = Path.Combine(Shared.TestOutputDirectory, type.Namespace, type.Name);

    if (!Directory.Exists(outputDir))
    {
      Directory.CreateDirectory(outputDir);
    }

    return Path.Combine(outputDir, $"{test.Method.Name}.zip");
  }
}
