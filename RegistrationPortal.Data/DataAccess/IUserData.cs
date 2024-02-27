namespace RegistrationPortal.Data.DataAccess;

public interface IUserData
{
    Task<List<User>> GetUsers();
    Task<User> GetUser(string id);
    Task<User> GetUserFromAuthentication(string objectId);
    Task CreateUser(User user);
    Task UpdateUser(User user);
}
