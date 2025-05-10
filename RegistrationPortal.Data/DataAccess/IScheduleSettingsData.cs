
namespace RegistrationPortal.Data.DataAccess;

public interface IScheduleSettingsData
{
    Task<ScheduleSettings> GetSettingsAsync();
    Task CreateSettingsAsync(ScheduleSettings settings);
    Task UpdateSettingsAsync(ScheduleSettings settings);
}