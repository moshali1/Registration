namespace RegistrationPortal.Shared;
public class StatusEnums
{
    public static readonly StatusEnums AwaitingReview = new StatusEnums("Awaiting Review");
    public static readonly StatusEnums Reviewed = new StatusEnums("Reviewed");
    public static readonly StatusEnums Verified = new StatusEnums("Verified");
    public static readonly StatusEnums Withdrawn = new StatusEnums("Withdrawn");
    public static readonly StatusEnums Disqualified = new StatusEnums("Disqualified");
    public static readonly StatusEnums Pending = new StatusEnums("Pending");

    public string Value { get; private set; }
    private StatusEnums(string value)
    {
        Value = value;
    }

}
