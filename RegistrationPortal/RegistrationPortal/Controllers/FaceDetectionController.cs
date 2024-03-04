using Microsoft.AspNetCore.Authorization;

namespace RegistrationPortal.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class FaceDetectionController : ControllerBase
{
    private readonly FaceDetectionService _faceService;

    public FaceDetectionController(FaceDetectionService faceService)
    {
        _faceService = faceService;
    }

    [HttpPost("detect-face")]
    public async Task<IActionResult> DetectFace([FromForm] IFormFile photo)
    {
        if (photo == null || photo.Length == 0)
        {
            return BadRequest("No file was uploaded.");
        }

        using (var stream = photo.OpenReadStream())
        {
            bool isFaceDetected = await _faceService.DetectFaceAsync(stream);
            return Ok(isFaceDetected);
        }
    }
}
