namespace RegistrationPortal;

public interface IFaceDetectionService
{
    Task<bool> DetectFaceAsync(Stream imageStream);
}
