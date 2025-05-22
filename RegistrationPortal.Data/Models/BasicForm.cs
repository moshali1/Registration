namespace RegistrationPortal.Data.Models;
public class BasicForm
{
    public string Id { get; set; }

    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public Gender Gender { get; set; }
    public DateOnly? DOB { get; set; }

    public string Country { get; set; }
    public string StateProvince { get; set; }
    public string City { get; set; }

    public string Category { get; set; }
    public string Portion { get; set; }
    public string Division { get; set; }

    public string Status { get; set; }
    public bool CheckedPrelimSchedule { get; set; } = false;
    public bool CheckedFinalSchedule { get; set; } = false;

    public string GetRegionalAddress()
    {
        return $"{City}, {StateProvince}, {Country}";
    }

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
