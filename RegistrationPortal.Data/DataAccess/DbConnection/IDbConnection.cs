namespace RegistrationPortal.Data.DataAccess;

public interface IDbConnection
{
    MongoClient Client { get; }
    string DbName { get; }
    IMongoCollection<Form> FormCollection { get; }
    string FormCollectionName { get; }
    IMongoCollection<Settings> SettingsCollection { get; }
    string SettingsCollectionName { get; }
    IMongoCollection<User> UserCollection { get; }
    string UserCollectionName { get; }
    IMongoCollection<EmailTemplate> EmailTemplateCollection { get; }
    string EmailTemplateCollectionName { get; }
    IMongoCollection<ScheduleSlot> SlotCollection { get; }
    string SlotCollectionName { get; }
    IMongoCollection<ScheduleSettings> ScheduleSettingsCollection { get; }
    string ScheduleSettingsCollectionName { get; }
}