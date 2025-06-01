namespace RegistrationPortal.Data.Models;
public class EventScheduleInfo
{
    public string PrelimScheduledDate { get; set; }
    public string PrelimScheduledTime { get; set; }
    public bool CheckedPrelimSchedule { get; set; } = false;
    public bool IsAttended { get; set; } = false;
    public bool IsQualified { get; set; } = false;
    public string FinalScheduledDate { get; set; }
    public string FinalScheduledTime { get; set; }
    public bool CheckedPrelimResults { get; set; } = false;
    public bool CheckedFinalSchedule { get; set; } = false;
    public DateTime CheckInDateTime { get; set; }

    public string ScheduleSlotId { get; set; }  // Reference to the selected slot
    public DateTime? ScheduleSelectionTime { get; set; }  // When the selection was made
}
