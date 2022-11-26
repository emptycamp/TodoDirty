namespace Todo.Shared.Responses.Auth;

public record AccessTokenResponse
{
    public required string Token { get; set; }
    public required string RefreshToken { get; set; }
}