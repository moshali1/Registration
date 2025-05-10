using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net;

namespace RegistrationPortal.Services;

public class EmailService : IEmailService
{
    private readonly string _apiKey;
    private readonly ILogger<EmailService> _logger;

    public EmailService(IConfiguration config, ILogger<EmailService> logger)
    {
        _apiKey = config["SendGrid:Key"];
        _logger = logger;
    }

    public async Task<Response> SendEmailAsync(string toEmail, string displayName, string subject, string plainTextContent, string htmlContent)
    {
        var client = new SendGridClient(_apiKey);
        var from = new EmailAddress("contact@imamshatibi.org", "Imam Al-Shatibi Quran Competition");
        var to = new EmailAddress(toEmail, displayName);
        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

        try
        {
            return await client.SendEmailAsync(msg);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error sending email to {Email}", toEmail);

            var errorContent = new StringContent(ex.Message);
            var dummyHttpResponse = new HttpResponseMessage();
            return new Response(
                HttpStatusCode.InternalServerError,
                errorContent,
                dummyHttpResponse.Headers
            );
        }
    }
}
