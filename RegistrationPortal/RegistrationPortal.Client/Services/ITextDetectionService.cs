
using Microsoft.AspNetCore.Components.Forms;

namespace RegistrationPortal;
// Class doesn't have to be in Client project. It was never in there. 
public interface ITextDetectionService
{
    Task<bool> DetectTextAsync(IBrowserFile file);
}