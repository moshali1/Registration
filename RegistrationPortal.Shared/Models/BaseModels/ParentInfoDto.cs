namespace RegistrationPortal.Shared.Models;
public class ParentInfoDto
{
    [Required, MaxLength(50, ErrorMessage = "Parent First Name must be a maximum of 50 characters.")]
    [Display(Name = "Parent First Name")]
    public string ParentFirstName { get; set; }

    [Required, MaxLength(50, ErrorMessage = "Parent Last Name must be a maximum of 50 characters.")]
    [Display(Name = "Parent Last Name")]
    public string ParentLastName { get; set; }

    [Required]
    [Display(Name = "Parent Phone Number")]
    [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
    public string ParentPhoneNumber { get; set; }

    public string GetParentFullName()
    {
        return $"{ParentFirstName} {ParentLastName}";
    }
}
