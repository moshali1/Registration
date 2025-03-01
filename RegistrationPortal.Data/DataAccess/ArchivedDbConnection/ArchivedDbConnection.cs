using Microsoft.Extensions.Configuration;

namespace RegistrationPortal.Data.DataAccess;
public class ArchivedDbConnection : IArchivedDbConnection
{
    private readonly IConfiguration _config;
    private readonly IMongoDatabase _db;
    private string _connectionId = "MongoDB";

    public string DbName { get; private set; }
    public string FormCollectionName { get; private set; } = "forms";
    public string UserCollectionName { get; private set; } = "users";


    public MongoClient Client { get; private set; }
    public IMongoCollection<Form> FormCollection { get; private set; }
    public IMongoCollection<User> UserCollection { get; private set; }

    public ArchivedDbConnection(IConfiguration config)
    {
        _config = config;
        Client = new MongoClient(_config.GetConnectionString(_connectionId));
        // TODO: Implement a way to get past year's data without hardcoding the year
        DbName = _config["ArchivedDatabaseName"];
        _db = Client.GetDatabase(DbName);

        FormCollection = _db.GetCollection<Form>(FormCollectionName);
        UserCollection = _db.GetCollection<User>(UserCollectionName);
    }
}
