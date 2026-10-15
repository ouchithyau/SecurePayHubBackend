using SecurePayHub.Domain.Entities;

namespace SecurePayHub.Application.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateAccessToken(User user);
        string GenerateRefreshToken();
    }
}