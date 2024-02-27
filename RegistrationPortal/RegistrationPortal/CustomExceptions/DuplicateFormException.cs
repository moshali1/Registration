namespace RegistrationPortal.CustomExceptions;
/// <summary>
/// Exception thrown when duplicate forms are detected.
/// </summary>
public class DuplicateFormException : Exception
{
    public DuplicateFormException(string message) : base(message)
    {
    }
}
