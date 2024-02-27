using System.Net.Http.Json;

namespace RegistrationPortal.Client.Services;

public class UserService : IUserService
{
    private readonly HttpClient _httpClient;

    public UserService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<UserDto> GetUserFromAuthentication(string objectId)
    {
        return await _httpClient.GetFromJsonAsync<UserDto>($"api/user/from-auth/{objectId}");
    }
}
