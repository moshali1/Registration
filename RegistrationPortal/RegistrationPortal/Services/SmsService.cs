using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace RegistrationPortal.Services;

public class SmsService
{
    private readonly string _accountSid;
    private readonly string _authToken;
    private readonly string _fromNumber;

    public SmsService(IConfiguration configuration)
    {
        _accountSid = configuration["Twilio:AccountSid"];
        _authToken = configuration["Twilio:AuthToken"];
        _fromNumber = configuration["Twilio:FromNumber"]; // Your ported phone number
    }

    public void SendSms(string toNumber, string message)
    {
        TwilioClient.Init(_accountSid, _authToken);

        var messageOptions = new CreateMessageOptions(new PhoneNumber(toNumber))
        {
            From = new PhoneNumber(_fromNumber),
            Body = message
        };

        var msg = MessageResource.Create(messageOptions);
        Console.WriteLine(msg.Sid); // Log the message SID for tracking
    }
}
