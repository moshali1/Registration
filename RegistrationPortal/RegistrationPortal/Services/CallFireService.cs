using CallfireApiClient;
using CallfireApiClient.Api.CallsTexts.Model;
using CallfireApiClient.Api.CallsTexts.Model.Request;

namespace RegistrationPortal.Services;

public class CallFireService
{
    private readonly string _apiLogin;
    private readonly string _apiPassword;

    public CallFireService(IConfiguration config)
    {
        _apiLogin = config["CallFire:Login"];
        _apiPassword = config["CallFire:Password"];
        
    }

    public void SendSms(string toNumber, string message)
    {
        var client = new CallfireClient(_apiLogin, _apiPassword);

        var recipient = new TextRecipient
        {
            PhoneNumber = toNumber,
            Message = message
        };

        var request = new SendTextsRequest
        {
            Recipients = new List<TextRecipient> { recipient }
        };

        try
        {
            var texts = client.TextsApi.Send(request);
        }
        catch (CallfireApiException e)
        {
            Console.WriteLine($"Callfire API error: {e.Message}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred: {e.Message}");
        }
    }
}
