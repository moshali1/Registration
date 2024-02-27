namespace RegistrationPortal.Shared;

public class NotificationText
{
    public string Title { get; set; }
    public string Message { get; set; }

    public NotificationText(string title, string message)
    {
        Title = title;
        Message = message;
    }
}
