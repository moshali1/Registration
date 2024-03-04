using SendGrid;

namespace RegistrationPortal.Services;
public interface IEmailService
{
    Task<Response> SendEmailAsync(string toEmail, string displayName, string subject, string plainTextContent, string htmlContent);
}