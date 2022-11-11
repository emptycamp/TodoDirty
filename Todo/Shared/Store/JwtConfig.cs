namespace Todo.Server.Store
{
    public class JwtConfig
    {
        public required string ValidIssuer { get; set; }
        public required string ValidAudience { get; set; }
        public required string Secret { get; set; }
        public required double ExpiresIn { get; set; }
    }

}
