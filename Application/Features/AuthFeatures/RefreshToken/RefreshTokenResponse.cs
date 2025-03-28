namespace Application.Features.AuthFeatures.RefreshToken;

public sealed record RefreshTokenResponse
{
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
}