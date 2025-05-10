namespace RegistrationPortal.Data.Models;
public class ScheduleSettings
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    // General scheduling information
    public string EventName { get; set; } = "Preliminary Round";
    public string EventDescription { get; set; } = "Schedule your time for the preliminary round competition.";

    // Instructions that will be shown to users
    public string SchedulingInstructions { get; set; } = "Please select a time slot that works best for you.";

    // Controls whether scheduling is currently enabled for users
    public bool IsSchedulingEnabled { get; set; } = true;

    // Format information
    public string TimeZoneDisplay { get; set; } = "Central Time (CT)";

    // Auto-update interval in seconds (for UI)
    public int AutoRefreshInterval { get; set; } = 10;

    // Date and time when scheduling begins and ends
    public DateTime SchedulingStartDate { get; set; } = DateTime.UtcNow;
    public DateTime SchedulingEndDate { get; set; } = DateTime.UtcNow.AddDays(14);

    // Email notification settings
    public bool SendConfirmationEmail { get; set; } = true;
    public string ConfirmationEmailTemplateId { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime ModifiedDate { get; set; } = DateTime.UtcNow;
}