using Domain.Entity.Identity;

namespace Application.Services;

public interface IAuthService
{
    Task<(string accessToken, string refreshToken)> GenerateTokensAsync(AppUser user);
    Task<(string accessToken, string refreshToken)> RefreshTokenAsync(string token, string ipAddress);
    Task RevokeTokenAsync(string token, string ipAddress);
    Task<bool> ValidateTokenAsync(string token);
}