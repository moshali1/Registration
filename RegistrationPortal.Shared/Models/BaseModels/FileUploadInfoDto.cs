using Microsoft.AspNetCore.Components.Forms;

namespace RegistrationPortal.Shared.Models;
public class FileUploadInfoDto
{

    [Required(ErrorMessage = "Please upload an ID")]
    public string IDFileName { get; set; }

    [Required(ErrorMessage = "Please upload a Photo")]
    public string PhotoFileName { get; set; }
    public string VideoFileName { get; set; }

    // File sizes (in bytes) and extensions 
    public double IDFileSize { get; set; }
    public string IDFileExtension { get; set; }
    public double PhotoFileSize { get; set; }
    public string PhotoFileExtension { get; set; }
    public double VideoFileSize { get; set; }
    public string VideoFileExtension { get; set; }


    public List<string> ReplacedFiles { get; set; } 
}
