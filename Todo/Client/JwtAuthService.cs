using Blazored.LocalStorage;
using System.Security.Claims;
using System.Text.Json;
using Todo.Shared.Responses.Auth;

namespace Todo.Client;

public class JwtAuthService
{
    private readonly ILocalStorageService _localStorage;

    public JwtAuthService(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    public async Task<AccessTokenResponse> GetTokenFromLocalStorage()
    {
        var accessToken = await _localStorage.GetItemAsStringAsync("token");
        var refreshToken = await _localStorage.GetItemAsStringAsync("refresh");

        return new AccessTokenResponse
        {
            Token = accessToken,
            RefreshToken = refreshToken
        };
    }

    public async Task StoreTokenToLocalStorage(string token, string refresh)
    {
        await _localStorage.SetItemAsStringAsync("token", token);
        await _localStorage.SetItemAsStringAsync("refresh", refresh);
    }

    public static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var payload = jwt.Split('.')[1];
        var jsonBytes = ParseBase64WithoutPadding(payload);
        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
        return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
    }

    private static byte[] ParseBase64WithoutPadding(string base64)
    {
        var suffix = (base64.Length % 4) switch
        {
            2 => "==",
            3 => "=",
            _ => ""
        };

        return Convert.FromBase64String(base64 + suffix);
    }
}