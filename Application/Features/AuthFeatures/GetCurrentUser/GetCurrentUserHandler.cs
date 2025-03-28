// Application/Features/AuthFeatures/GetCurrentUser/GetCurrentUserHandler.cs
using Application.Common.Exceptions;
using Application.Repositories;
using Application.Services;
using AutoMapper;
using MediatR;

namespace Application.Features.AuthFeatures.GetCurrentUser;

public sealed class GetCurrentUserHandler : 
    BaseHandlerAuth, 
    IRequestHandler<GetCurrentUserRequest, GetCurrentUserResponse>
{
    private readonly ICurrentUserService _currentUserService;
    
    public GetCurrentUserHandler(
        IUserRepository userRepository, 
        IAuthService authService, 
        IMapper mapper,
        ICurrentUserService currentUserService) 
        : base(userRepository, authService, mapper)
    {
        _currentUserService = currentUserService;
    }

    public async Task<GetCurrentUserResponse> Handle(GetCurrentUserRequest request, CancellationToken cancellationToken)
    {
        if (!_currentUserService.IsAuthenticated || string.IsNullOrEmpty(_currentUserService.UserId))
        {
            throw new UnauthorizedAccessException("User tidak terautentikasi");
        }
        
        var user = await UserRepository.GetByIdAsync(_currentUserService.UserId);
        if (user == null)
        {
            throw new NullRequestException("User tidak ditemukan");
        }
        
        var roles = await UserRepository.GetRolesAsync(user);
        
        return new GetCurrentUserResponse
        {
            Id = user.Id,
            Username = user.UserName ?? string.Empty,
            Email = user.Email ?? string.Empty,
            FirstName = user.FirstName ?? string.Empty,
            LastName = user.LastName ?? string.Empty,
            Roles = roles
        };
    }
}