using MediatR;

namespace Application.Features.AuthFeatures.RevokeToken;

public sealed record RevokeTokenRequest(string Token) : IRequest<RevokeTokenResponse>;
