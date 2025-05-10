namespace RegistrationPortal.Data.Models;
public class ScheduleSlot
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public string Date { get; set; }
    public string StartTime { get; set; }
    public string EndTime { get; set; }

    public int MaxParticipants { get; set; }
    public int CurrentParticipants { get; set; } = 0;

    public bool IsEnabled { get; set; } = true;

    // These are used to filter slots based on the form's division and category
    public List<string> Divisions { get; set; } = new List<string>();
    public List<string> Categories { get; set; } = new List<string>();


    // Concurrency control
    public long Version { get; set; } = 0;

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime ModifiedDate { get; set; } = DateTime.UtcNow;

    public ScheduleSlot()
    {
        Divisions = new List<string>();
        Categories = new List<string>();
    }

    public bool IsApplicableFor(Form form)
    {
        // If divisions or categories are specified, check if the form matches
        bool divisionMatch = Divisions.Count == 0 || Divisions.Contains(form.CompetitionInfo.Division);
        bool categoryMatch = Categories.Count == 0 || Categories.Contains(form.CompetitionInfo.Category);

        return IsEnabled && divisionMatch && categoryMatch && CurrentParticipants < MaxParticipants;
    }
}
