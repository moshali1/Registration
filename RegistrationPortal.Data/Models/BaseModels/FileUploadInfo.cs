namespace RegistrationPortal.Data.Models;
public class FileUploadInfo
{
    public string IDFileName { get; set; }
    public string PhotoFileName { get; set; }
    public string VideoFileName { get; set; }

    // File sizes (in bytes)
    public double IDFileSize { get; set; }
    public string IDFileExtension { get; set; }
    public double PhotoFileSize { get; set; }
    public string PhotoFileExtension { get; set; }
    public double VideoFileSize { get; set; }
    public string VideoFileExtension { get; set; }

    public List<string> ReplacedFiles { get; set; }

}