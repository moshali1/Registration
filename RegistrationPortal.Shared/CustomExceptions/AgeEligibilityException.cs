namespace RegistrationPortal.Shared;
/// <summary>
/// Exception thrown when age eligibility requirements are not met.
/// </summary>
public class AgeEligibilityException : Exception
{
    public AgeEligibilityException(string message) : base(message)
    {
    }
}
