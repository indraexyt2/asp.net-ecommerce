using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Application.Common.Exceptions;
using Application.Services;
using Domain.Common;
using Domain.Entity.Identity;
using Infrastructure.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly JwtSettings _jwtSettings;
    private readonly UserManager<AppUser> _userManager;
    private readonly DataContext _context;

    public AuthService(IOptions<JwtSettings> jwtSettings, UserManager<AppUser> userManager, DataContext context)
    {
        _jwtSettings = jwtSettings.Value;
        _userManager = userManager;
        _context = context;
    }

    public async Task<(string accessToken, string refreshToken)> GenerateTokensAsync(AppUser user)
    {
        var accessToken = await GenerateJwtToken(user);
        var refreshTokenStr = GenerateRefreshToken();
        
        var refreshToken = new RefreshToken
        {
            Token = refreshTokenStr,
            Expires = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpiryInDays),
            Created = DateTime.UtcNow,
            CreatedByIp = "localhost",
            UserId = user.Id
        };

        _context.RefreshToken.Add(refreshToken);
        await _context.SaveChangesAsync();

        return (accessToken, refreshTokenStr);
    }

    public async Task<(string accessToken, string refreshToken)> RefreshTokenAsync(string token, string ipAddress)
    {
        var oldRefreshToken = await _context.RefreshToken
            .Include(r => r.User)
            .SingleOrDefaultAsync(r => r.Token == token);
    
        if (oldRefreshToken == null)
            throw new NullRequestException("Token tidak valid");
    
        if (oldRefreshToken.IsExpired)
            throw new BadRequestException("Token invalid", "Refresh token telah kadaluarsa");
    
        if (!oldRefreshToken.IsActive)
            throw new BadRequestException("Token invalid", "Refresh token tidak aktif");
        
        var user = oldRefreshToken.User;
        if (user == null)
            throw new NullRequestException("User tidak ditemukan");
        
        oldRefreshToken.Revoked = DateTime.UtcNow;
        oldRefreshToken.RevokedByIp = ipAddress;
        
        var newRefreshTokenString = GenerateRefreshToken();
        oldRefreshToken.ReplacedByToken = newRefreshTokenString;
        
        _context.RefreshToken.Update(oldRefreshToken);
        
        var newRefreshToken = new RefreshToken
        {
            Token = newRefreshTokenString,
            UserId = user.Id,
            Expires = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpiryInDays),
            Created = DateTime.UtcNow,
            CreatedByIp = ipAddress
        };
        
        _context.RefreshToken.Add(newRefreshToken);
        await _context.SaveChangesAsync();
        var accessToken = await GenerateJwtToken(user);
    
        return (accessToken, newRefreshTokenString);
    }

    public async Task RevokeTokenAsync(string token, string ipAddress)
    {
        var user = await GetUserByRefreshToken(token);
        var refreshToken = user.RefreshTokens.Single(r => r.Token == token);

        if (!refreshToken.IsActive)
            throw new BadRequestException("Token invalid", "Refresh token tidak aktif");

        // Revoke token
        refreshToken.Revoked = DateTime.UtcNow;
        refreshToken.RevokedByIp = ipAddress;

        await _userManager.UpdateAsync(user);
    }

    public async Task<bool> ValidateTokenAsync(string token)
    {
        if (string.IsNullOrEmpty(token))
            return false;

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey);

        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = _jwtSettings.Issuer,
                ValidateAudience = true,
                ValidAudience = _jwtSettings.Audience,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            return true;
        }
        catch
        {
            return false;
        }
    }

    private async Task<string> GenerateJwtToken(AppUser user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey);

        var userRoles = await _userManager.GetRolesAsync(user);
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName ?? string.Empty),
            new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        // Tambahkan claims untuk role
        foreach (var role in userRoles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpiryInMinutes),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    private async Task<AppUser> GetUserByRefreshToken(string token)
    {
        var user = await _context.Users
            .Include(u => u.RefreshTokens)
            .SingleOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == token));

        if (user == null)
            throw new NullRequestException("Token tidak valid");

        return user;
    }
}