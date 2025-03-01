namespace RegistrationPortal.Data.DataAccess;

public interface IArchivedDbConnection
{
    MongoClient Client { get; }
    string DbName { get; }
    IMongoCollection<Form> FormCollection { get; }
    string FormCollectionName { get; }
    IMongoCollection<User> UserCollection { get; }
    string UserCollectionName { get; }
}