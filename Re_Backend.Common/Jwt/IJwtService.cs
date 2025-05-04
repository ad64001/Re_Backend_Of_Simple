namespace Re_Backend.Common.Jwt
{
    public interface IJwtService
    {
        string GenerateToken(string userId, string role);
        string ParseToken(string token);
    }
}
