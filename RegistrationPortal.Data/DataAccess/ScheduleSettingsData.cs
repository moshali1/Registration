namespace RegistrationPortal.Data.DataAccess;
public class ScheduleSettingsData : IScheduleSettingsData
{
    private readonly IMongoCollection<ScheduleSettings> _settings;

    public ScheduleSettingsData(IDbConnection db)
    {
        _settings = db.ScheduleSettingsCollection;
    }

    public async Task<ScheduleSettings> GetSettingsAsync()
    {
        var settings = await _settings.Find(_ => true).FirstOrDefaultAsync();
        if (settings == null)
        {
            // Create default settings if none exist
            settings = new ScheduleSettings();
            await CreateSettingsAsync(settings);
        }
        return settings;
    }

    public async Task CreateSettingsAsync(ScheduleSettings settings)
    {
        settings.CreatedDate = DateTime.UtcNow;
        settings.ModifiedDate = DateTime.UtcNow;
        await _settings.InsertOneAsync(settings);
    }

    public async Task UpdateSettingsAsync(ScheduleSettings settings)
    {
        settings.ModifiedDate = DateTime.UtcNow;
        await _settings.ReplaceOneAsync(s => s.Id == settings.Id, settings);
    }
}