using System.ComponentModel.DataAnnotations;

namespace RegistrationPortal.Data.Models;
public class Competitor
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string RID { get; set; } = default!; // Registration GUID
    public int CID { get; set; } // Competitor ID 

    [Required]
    public string FirstName { get; set; } = default!;
    public string? MiddleName { get; set; }
    [Required]
    public string LastName { get; set; } = default!;
    [Required]
    public Gender Gender { get; set; }
    public DateTime? DOB { get; set; } // Nullable to prevent default value (01/01/0001) and allow the input to display as empty (mm/dd/yyyy).
    public string Country { get; set; } = default!;
    public string Region { get; set; } = default!; // State or Province
    public string City { get; set; } = default!;


    [Required]
    public string Division { get; set; } = default!;
    [Required]
    public string Category { get; set; } = default!;
    public int? Level { get; set; } // Nullable to allow the level field to remain unset initially, ensuring the input can display as empty.
    [Required]
    public string Portion { get; set; } = default!;

    public string PrelimQuestionSetId { get; set; } = default!;

    // Implement preliminary and final schedule dates and times.
    // This will require a mechanism to store two sets of grades: one for prelims and one for finals.
    public DateTime ScheduledDateTime { get; set; }
    public string PrelimScheduledDate { get; set; } = default!;
    public string PrelimScheduledTime { get; set; } = default!;

    public string FinalScheduledDate { get; set; } = default!;
    public string FinalScheduledTime { get; set; } = default!;
    public bool AttendedI { get; set; }
    public bool IsQualify { get; set; }
    public bool AttendedII { get; set; }

    public DateTime CheckInDateTime { get; set; }


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

    public string GetRegionalAddress()
    {
        if (Region == "NA")
        {
            return $"{City}, {Country}";
        }
        else
        {
            return $"{Region}, {Country}";
        }
    }

    public bool IsFromMN()
    {
        if (Region == "MN")
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
