using Microsoft.AspNetCore.Components.Authorization;

namespace RegistrationPortal.Shared.Helpers;

public static class AuthenticationStateProviderHelpers
{
    public static async Task<UserDto> GetUserFromAuth(
        this AuthenticationStateProvider provider,
        IUserService userData)
    {
        var authState = await provider.GetAuthenticationStateAsync();
        string objectId = authState.User.Claims.FirstOrDefault(c => c.Type.Contains("objectidentifier"))?.Value;
        return await userData.GetUserFromAuthentication(objectId);
    }
}