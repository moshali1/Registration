namespace RegistrationPortal.Data.Models;
public class PersonalInfo
{
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public Gender Gender { get; set; }
    public DateOnly? DOB { get; set; }

    public string GetFullName()
    {
        return $"{FirstName} {LastName}";
    }

    public string GetFirstMiddleLastName()
    {
        if (MiddleName is not null)
        {
            return $"{FirstName} {MiddleName} {LastName}";
        }
        else
        {
            return GetFullName();
        }

    }
}

public enum Gender
{
    Male,
    Female
}
