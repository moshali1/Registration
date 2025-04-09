namespace RegistrationPortal.Data.DataAccess;
public class UserData : IUserData
{
    private readonly IMongoCollection<User> _users;

    public UserData(IDbConnection db) =>
        _users = db.UserCollection;

    public async Task<List<User>> GetUsers() =>
        await _users.Find(_ => true).ToListAsync();

    public async Task<User> GetUser(string id) =>
        await _users.Find(u => u.Id == id).FirstOrDefaultAsync();

    public async Task<User> GetUserByEmail(string emailAddress) =>
        await _users.Find(u => u.EmailAddress == emailAddress).FirstOrDefaultAsync();

    public async Task<User> GetUserFromAuthentication(string objectId) =>
        await _users.Find(u => u.ObjectIdentifier == objectId).FirstOrDefaultAsync();

    public async Task CreateUser(User user) =>
        await _users.InsertOneAsync(user);

    public async Task UpdateUser(User user) =>
        await _users.ReplaceOneAsync(u => u.Id == user.Id, user);
}
