using System.Net.Http.Headers;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace Todo.Client
{
    public class JwtAuthStateProvider : AuthenticationStateProvider
    {
        private readonly JwtAuthService _jwtAuthService;
        private readonly HttpClient _httpClient;

        public JwtAuthStateProvider(JwtAuthService jwtAuthService, HttpClient httpClient)
        {
            _jwtAuthService = jwtAuthService;
            _httpClient = httpClient;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = (await _jwtAuthService.GetTokenFromLocalStorage()).Token;
            return UpdateAuthenticationState(token);
        }

        private AuthenticationState UpdateAuthenticationState(string? token)
        {
            var identity = new ClaimsIdentity();
            _httpClient.DefaultRequestHeaders.Authorization = null;

            if (!string.IsNullOrEmpty(token))
            {
                identity = new ClaimsIdentity(JwtAuthService.ParseClaimsFromJwt(token), "jwt");
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token.Replace("\"", ""));
            }

            var user = new ClaimsPrincipal(identity);
            var state = new AuthenticationState(user);

            NotifyAuthenticationStateChanged(Task.FromResult(state));

            return state;
        }
    }
}
