using Application.Common.Exceptions;
using Application.Repositories;
using Application.Services;
using AutoMapper;
using MediatR;

namespace Application.Features.AuthFeatures.LoginUser;

public class LoginUserHandler : BaseHandlerAuth, IRequestHandler<LoginUserRequest, LoginUserResponse>
{
    public LoginUserHandler(IUserRepository userRepository, IAuthService authService, IMapper mapper) : base(userRepository, authService, mapper)
    {
    }
    
    public async Task<LoginUserResponse> Handle(LoginUserRequest request, CancellationToken cancellationToken)
    {
        // Cari user berdasarkan username
        var user = await UserRepository.GetByUsernameAsync(request.Username);
        if (user == null)
        {
            throw new BadRequestException("Login gagal", "Username atau password salah");
        }
        
        // Verifikasi password
        var isPasswordValid = await UserRepository.CheckPasswordAsync(user, request.Password);
        if (!isPasswordValid)
        {
            throw new BadRequestException("Login gagal", "Username atau password salah");
        }
        
        // Generate token
        var (accessToken, refreshToken) = await AuthService.GenerateTokensAsync(user);
        
        // Ambil roles
        var roles = await UserRepository.GetRolesAsync(user);
        
        // Mapping ke response
        var response = Mapper.Map<LoginUserResponse>(user);
        response.Roles = roles;
        response.AccessToken = accessToken;
        response.RefreshToken = refreshToken;
        
        return response;
    }
}