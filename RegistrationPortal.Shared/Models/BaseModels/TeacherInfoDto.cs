namespace RegistrationPortal.Shared.Models;
public class TeacherInfoDto
{
    [Required, MaxLength(50, ErrorMessage = "Teacher First Name must be a maximum of 50 characters.")]
    [Display(Name = "Teacher First Name")]
    public string TeacherFirstName { get; set; }

    [Required, MaxLength(50, ErrorMessage = "Teacher Last Name must be a maximum of 50 characters.")]
    [Display(Name = "Teacher Last Name")]
    public string TeacherLastName { get; set; }

    [Required]
    [Display(Name = "Teacher Phone Number")]
    [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
    public string TeacherPhoneNumber { get; set; }

    [Required, MaxLength(100, ErrorMessage = "Institution must be a maximum of 100 characters.")]
    public string Institution { get; set; }

    public string GetTeacherFullName()
    {
        return $"{TeacherFirstName} {TeacherLastName}";
    }

}
