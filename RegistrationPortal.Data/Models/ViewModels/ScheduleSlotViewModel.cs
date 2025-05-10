namespace RegistrationPortal.Data.Models.ViewModels;
public class ScheduleSlotViewModel
{
    public string Id { get; set; }
    public string Date { get; set; }
    public string StartTime { get; set; }
    public string EndTime { get; set; }
    public int MaxParticipants { get; set; }
    public int CurrentParticipants { get; set; }
    public bool IsEnabled { get; set; }
    public bool IsAvailable => IsEnabled && CurrentParticipants < MaxParticipants;
    public int ActiveViewers { get; set; }

    public string DisplayText => $"{Date} | {StartTime} - {EndTime} | {CurrentParticipants}/{MaxParticipants}";
}