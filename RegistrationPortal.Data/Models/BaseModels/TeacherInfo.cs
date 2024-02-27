namespace RegistrationPortal.Data.Models;
public class TeacherInfo
{
    public string TeacherFirstName { get; set; }
    public string TeacherLastName { get; set; }
    public string TeacherPhoneNumber { get; set; }
    public string Institution { get; set; }

    public string GetTeacherFullName()
    {
        return $"{TeacherFirstName} {TeacherLastName}";
    }
}
