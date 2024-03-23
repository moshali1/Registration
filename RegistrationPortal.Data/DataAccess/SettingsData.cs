namespace RegistrationPortal.Data.DataAccess;
public class SettingsData : ISettingsData
{
    private readonly IMongoCollection<Settings> _settings;

    public SettingsData(IDbConnection db) =>
        _settings = db.SettingsCollection;

    public async Task<Settings> GetSettings() =>
        await _settings.Find(_ => true).FirstOrDefaultAsync();

    public async Task CreateSettings(Settings settings) =>
        await _settings.InsertOneAsync(settings);

    public async Task UpdateSettings(Settings settings) =>
        await _settings.ReplaceOneAsync(u => u.Id == settings.Id, settings);
}
