
namespace RegistrationPortal.Data.DataAccess;

public interface IScheduleSlotData
{
    Task<List<ScheduleSlot>> GetAllSlotsAsync();
    Task<List<ScheduleSlot>> GetAvailableSlotsAsync(string division = null, string category = null);
    Task<ScheduleSlot> GetSlotByIdAsync(string id);
    Task<bool> IsSlotAvailableAsync(string id);
    Task<bool> ReserveSlotAsync(string slotId, string formId);
    Task<bool> CancelReservationAsync(string slotId, string formId);
    Task CreateSlotAsync(ScheduleSlot slot);
    Task UpdateSlotAsync(ScheduleSlot slot);
    Task DeleteSlotAsync(string id);
    Task<int> GetParticipantCountForSlotAsync(string slotId);
}