using EventHub.Models;

namespace EventHub.Jwt
{
    public interface ITokenManager
    {
        string GenerateToken(User user, string roleName);

    }
}
