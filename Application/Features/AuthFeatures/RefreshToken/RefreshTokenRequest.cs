using MediatR;

namespace Application.Features.AuthFeatures.RefreshToken;

public sealed record RefreshTokenRequest(string Token) : IRequest<RefreshTokenResponse>;