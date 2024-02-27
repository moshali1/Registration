namespace RegistrationPortal.CustomExceptions;
/// <summary>
/// Exception thrown when form data validation fails.
/// </summary>
public class FormDataValidationException : Exception
{
    public FormDataValidationException(string message) : base(message)
    {
    }
}
