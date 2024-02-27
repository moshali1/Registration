namespace RegistrationPortal;

public interface IUserService
{
    Task<UserDto> GetUserFromAuthentication(string objectId);
}
