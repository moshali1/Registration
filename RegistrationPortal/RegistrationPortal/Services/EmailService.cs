//using SendGrid.Helpers.Mail;
//using SendGrid;

//namespace RegistrationPortal.Services;

//public class EmailService : IEmailService
//{
//    private readonly string _sendGridApiKey;

//    public EmailService(IConfiguration config)
//    {
//        _sendGridApiKey = config.GetValue<string>("SendGrid:Key");
//    }

//    public async Task<Response> SendEmailAsync(string toEmail, string subject, string messageBody)
//    {
//        var client = new SendGridClient(_sendGridApiKey);
//        var from = new EmailAddress("contact@imamshatibi.org", "Imam Al-Shatibi Quran Competition");
//        var to = new EmailAddress(toEmail);
//        var msg = MailHelper.CreateSingleEmail(from, to, subject, messageBody, messageBody);
//        var response = await client.SendEmailAsync(msg);
//        await Console.Out.WriteLineAsync(toEmail + " " + subject + " " + messageBody.ToString());
//        return response;
//    }
//}
