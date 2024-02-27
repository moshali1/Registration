namespace RegistrationPortal.Data.Models;
public class EventScheduleInfo
{
    public string PrelimScheduledDate { get; set; }
    public string PrelimScheduledTime { get; set; }
    public bool IsAttended { get; set; } = false;
    public bool IsQualified { get; set; } = false;
    public string FinalScheduledDate { get; set; }
    public string FinalScheduledTime { get; set; }
    public DateTime CheckInDateTime { get; set; }
}
