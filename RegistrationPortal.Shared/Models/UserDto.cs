namespace RegistrationPortal.Shared.Models;
public class UserDto
{
    public string Id { get; set; }
    public string ObjectIdentifier { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string DisplayName { get; set; }
    public string EmailAddress { get; set; }
    public string Role { get; set; }

    public string GetFullName()
    {
        return $"{FirstName} {LastName}";
    }
}
