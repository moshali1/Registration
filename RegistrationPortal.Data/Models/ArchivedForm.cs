namespace RegistrationPortal.Data.Models;
public class ArchivedForm
{
    public string CreatorId { get; set; }
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public Gender Gender { get; set; }
    public DateTime DOB { get; set; }
    public string Country { get; set; }
    public string State { get; set; }
    public string City { get; set; }
    public string ParentFirstName { get; set; }
    public string ParentLastName { get; set; }
    public string ParentPhoneNumber { get; set; }
    public string TeacherFirstName { get; set; }
    public string TeacherLastName { get; set; }
    public string TeacherPhoneNumber { get; set; }
    public string Institution { get; set; }
    public string Division { get; set; }
    public string Category { get; set; }
    public string CategoryType { get; set; }
}
