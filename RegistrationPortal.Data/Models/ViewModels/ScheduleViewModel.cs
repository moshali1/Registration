namespace RegistrationPortal.Data.Models.ViewModels;
public class ScheduleViewModel
{
    public ScheduleSettings Settings { get; set; }
    public List<ScheduleSlotViewModel> AvailableSlots { get; set; }
    public string SelectedSlotId { get; set; }
    public string FormId { get; set; }
    public string Division { get; set; }
    public string Category { get; set; }
    public string Error { get; set; }
    public string Message { get; set; }
}