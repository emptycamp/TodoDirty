namespace Todo.Shared.Responses
{
    public record JwtTokenResponse
    {
        public required string Token { get; set; }
    }
}
