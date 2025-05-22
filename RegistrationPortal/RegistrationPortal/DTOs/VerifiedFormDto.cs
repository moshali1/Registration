namespace RegistrationPortal.DTOs;

public class VerifiedFormDto
{
    public string RID { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string? MiddleName { get; set; }
    public string LastName { get; set; } = null!;
    public Data.Models.Gender Gender { get; set; }
    public DateOnly? DOB { get; set; }
    public string Country { get; set; } = null!;
    public string Region { get; set; } = null!;
    public string City { get; set; } = null!;
    public string Division { get; set; } = null!;
    public string Category { get; set; } = null!;
    public string Portion { get; set; } = null!;
    public string PrelimScheduledDate { get; set; } = null!;
    public string PrelimScheduledTime { get; set; } = null!;

}
