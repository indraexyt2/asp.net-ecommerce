using Application.Repositories;
using Application.Services;
using AutoMapper;
using MediatR;

namespace Application.Features.AuthFeatures.RefreshToken;

public sealed class RefreshTokenHandler : 
    BaseHandlerAuth, 
    IRequestHandler<RefreshTokenRequest, RefreshTokenResponse>
{
    public RefreshTokenHandler(IUserRepository userRepository, IAuthService authService, IMapper mapper) 
        : base(userRepository, authService, mapper)
    {
    }

    public async Task<RefreshTokenResponse> Handle(RefreshTokenRequest request, CancellationToken cancellationToken)
    {
        var ipAddress = "localhost";
        
        var (accessToken, refreshToken) = await AuthService.RefreshTokenAsync(request.Token, ipAddress);
        
        return new RefreshTokenResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }
}