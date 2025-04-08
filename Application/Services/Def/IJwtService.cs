using Infrastructure.Entities;

namespace Application.Services.Def;

public interface IJwtService
{
    public string GenerateAccessToken(User user);
    public string GenerateRefreshToken(User user);
    public string? ValidateRefreshToken(string refreshToken);
}