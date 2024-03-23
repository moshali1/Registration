
namespace RegistrationPortal.Data.DataAccess;

public interface ISettingsData
{
    Task CreateSettings(Settings settings);
    Task<Settings> GetSettings();
    Task UpdateSettings(Settings settings);
}