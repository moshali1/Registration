namespace RegistrationPortal.Shared.Models;
public class PersonalInfoDto
{
    [Required, MaxLength(50, ErrorMessage = "First Name must be a maximum of 50 characters.")]
    [Display(Name = "First Name")]
    public string FirstName { get; set; }

    [Required, MaxLength(50, ErrorMessage = "Middle Name must be a maximum of 50 characters.")]
    public string MiddleName { get; set; }

    [Required, MaxLength(50, ErrorMessage = "Last Name must be a maximum of 50 characters.")]
    [Display(Name = "Last Name")]
    public string LastName { get; set; }

    [Required]
    public Gender Gender { get; set; }

    [Required]
    public DateOnly? DOB { get; set; }

    [Display(Name = "Phone Number")]
    [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
    public string PhoneNumber { get; set; }

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
    public string DOBUSFormat
    {
        get { return DOB?.ToString("MM-dd-yyyy"); }
    }
}

public enum Gender
{
    Male,
    Female
}
