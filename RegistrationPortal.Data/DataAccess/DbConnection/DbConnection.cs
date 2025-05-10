using Microsoft.Extensions.Configuration;

namespace RegistrationPortal.Data.DataAccess;
public class DbConnection : IDbConnection
{
    private readonly IConfiguration _config;
    private readonly IMongoDatabase _db;
    private string _connectionId = "MongoDB";

    public string DbName { get; private set; }
    public string FormCollectionName { get; private set; } = "forms";
    public string UserCollectionName { get; private set; } = "users";
    public string SettingsCollectionName { get; private set; } = "settings";
    public string EmailTemplateCollectionName { get; private set; } = "emailTemplates";
    public string SlotCollectionName { get; private set; } = "scheduleSlots";
    public string ScheduleSettingsCollectionName { get; private set; } = "scheduleSettings";


    public MongoClient Client { get; private set; }
    public IMongoCollection<Form> FormCollection { get; private set; }
    public IMongoCollection<User> UserCollection { get; private set; }
    public IMongoCollection<Settings> SettingsCollection { get; private set; }
    public IMongoCollection<EmailTemplate> EmailTemplateCollection { get; private set; }
    public IMongoCollection<ScheduleSlot> SlotCollection { get; private set; }
    public IMongoCollection<ScheduleSettings> ScheduleSettingsCollection { get; private set; }


    public DbConnection(IConfiguration config)
    {
        _config = config;
        Client = new MongoClient(_config.GetConnectionString(_connectionId));
        DbName = _config["DatabaseName"];
        _db = Client.GetDatabase(DbName);

        FormCollection = _db.GetCollection<Form>(FormCollectionName);
        UserCollection = _db.GetCollection<User>(UserCollectionName);
        SettingsCollection = _db.GetCollection<Settings>(SettingsCollectionName);
        EmailTemplateCollection = _db.GetCollection<EmailTemplate>(EmailTemplateCollectionName);
        SlotCollection = _db.GetCollection<ScheduleSlot>(SlotCollectionName);
        ScheduleSettingsCollection = _db.GetCollection<ScheduleSettings>(ScheduleSettingsCollectionName);
    }
}
