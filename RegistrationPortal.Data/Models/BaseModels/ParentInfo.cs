namespace RegistrationPortal.Data.Models;
public class ParentInfo
{
    public string ParentFirstName { get; set; }
    public string ParentLastName { get; set; }
    public string ParentPhoneNumber { get; set; }

    public string GetParentFullName()
    {
        return $"{ParentFirstName} {ParentLastName}";
    }
}
