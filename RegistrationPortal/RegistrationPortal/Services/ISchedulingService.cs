using RegistrationPortal.Data.Models.ViewModels;

namespace RegistrationPortal.Services;
public interface ISchedulingService
{
    Task AssignRandomSlotsToUnscheduledFormsAsync();
    Task<bool> CancelReservationAsync(string formId);
    Task CreateSlotAsync(ScheduleSlot slot);
    Task DeleteSlotAsync(string id);
    Task<List<ScheduleSlot>> GetAllSlotsAsync();
    Task<List<ScheduleSlotViewModel>> GetAvailableSlotsForFormAsync(string formId);
    Task<ScheduleSettings> GetScheduleSettingsAsync();
    Task<ScheduleViewModel> GetScheduleViewModelAsync(string formId, string division, string category);
    Task<ScheduleSlot> GetSlotByIdAsync(string id);
    Task<bool> IsPrelimScheduleSelectedAsync(string formId);
    Task<bool> ReserveSlotAsync(string formId, string slotId);
    Task UpdateScheduleSettingsAsync(ScheduleSettings settings);
    Task UpdateSlotAsync(ScheduleSlot slot);
}