using Application.Repositories;
using Application.Services;
using AutoMapper;
using MediatR;

namespace Application.Features.AuthFeatures.RevokeToken;

public sealed class RevokeTokenHandler : 
    BaseHandlerAuth, 
    IRequestHandler<RevokeTokenRequest, RevokeTokenResponse>
{
    public RevokeTokenHandler(IUserRepository userRepository, IAuthService authService, IMapper mapper) 
        : base(userRepository, authService, mapper)
    {
    }

    public async Task<RevokeTokenResponse> Handle(RevokeTokenRequest request, CancellationToken cancellationToken)
    {
        string ipAddress = "localhost";
        await AuthService.RevokeTokenAsync(request.Token, ipAddress);
        
        return new RevokeTokenResponse(true);
    }
}