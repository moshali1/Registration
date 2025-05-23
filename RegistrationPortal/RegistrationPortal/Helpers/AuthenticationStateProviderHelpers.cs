﻿using Microsoft.AspNetCore.Components.Authorization;

namespace RegistrationPortal.Helpers;

public static class AuthenticationStateProviderHelpers
{
    public static async Task<User> GetUserFromAuth(
        this AuthenticationStateProvider provider,
        IUserData userData)
    {
        var authState = await provider.GetAuthenticationStateAsync();
        string objectId = authState.User.Claims.FirstOrDefault(c => c.Type.Contains("objectidentifier"))?.Value;
        return await userData.GetUserFromAuthentication(objectId);
    }
}
