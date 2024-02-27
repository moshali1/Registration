namespace RegistrationPortal.Shared;

public class ErrorMappingService
{
    public static string MapServerMessageToClientMessage(string serverMessage)
    {
        Console.WriteLine(serverMessage);
        // Your mapping logic here, e.g., switch-case, if statements, or Dictionary lookup
        if (serverMessage.StartsWith("Form validation failed:"))
        {
            return "Validation Error";
        }
        else if (serverMessage == "The applicant does not meet the age requirements for the chosen category.")
        {
            return "Age Eligibility Error";
        }
        else if (serverMessage == "A form with the same personal and competition details already exists.")
        {
            return "Duplicate Entry Error";
        }
        else
        {
            return "Unexpected Error";
        }
    }
}