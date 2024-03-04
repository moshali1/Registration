using SendGrid.Helpers.Mail;
using SendGrid;

namespace RegistrationPortal.Services;

public class EmailService : IEmailService
{
    private readonly string _apiKey;

    public EmailService(IConfiguration config)
    {
        _apiKey = config["SendGrid:Key"];
    }

    public async Task<Response> SendEmailAsync(string toEmail, string displayName, string subject, string plainTextContent, string htmlContent)
    {
        var client = new SendGridClient(_apiKey);
        var from = new EmailAddress("contact@imamshatibi.org", "Imam Al-Shatibi Quran Competition");
        var to = new EmailAddress(toEmail, displayName);
        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
        var response = await client.SendEmailAsync(msg);
        return response;
    }
}
