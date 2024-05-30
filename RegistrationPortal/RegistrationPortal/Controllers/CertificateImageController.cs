namespace RegistrationPortal.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CertificateImageController : ControllerBase
{
    private readonly CertificateImageService _certificateImageService;

    public CertificateImageController(CertificateImageService certificateImageService)
    {
        _certificateImageService = certificateImageService;
    }

    [HttpGet("download")]
    public async Task<IActionResult> DownloadCertificateImage(string name, string category, string division, char gender)
    {
        var imageBytes = await _certificateImageService.GenerateCertificateImageAsync(name, category, division, gender);
        return File(imageBytes, "image/png", "Certificate.png");
    }
}
