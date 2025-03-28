using Application.Common.Exceptions;
using Application.Repositories;
using Application.Services;
using AutoMapper;
using Domain.Entity.Identity;
using MediatR;

namespace Application.Features.AuthFeatures.RegisterUser;

public class RegisterUserHandler : BaseHandlerAuth, IRequestHandler<RegisterUserRequest, RegisterUserResponse>
{
    public RegisterUserHandler(IUserRepository userRepository, IAuthService authService, IMapper mapper) : 
        base(userRepository, authService, mapper)
    {
    }

    public async Task<RegisterUserResponse> Handle(RegisterUserRequest request, CancellationToken cancellationToken)
    {
        // Cek apakah username sudah digunakan
        var existingUser = await UserRepository.GetByUsernameAsync(request.Username);
        if (existingUser != null)
        {
            throw new BadRequestException("Registrasi gagal", "Username sudah digunakan");
        }
        
        // Cek apakah email sudah digunakan
        existingUser = await UserRepository.GetByEmailAsync(request.Email);
        if (existingUser != null)
        {
            throw new BadRequestException("Registrasi gagal", "Email sudah digunakan");
        }

        // Buat user baru
        var user = new AppUser
        {
            UserName = request.Username,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            DateCreated = DateTimeOffset.UtcNow
        };

        // Simpan user dan set password
        var createdUser = await UserRepository.CreateUserAsync(user, request.Password);
        
        // Tambahkan ke role
        await UserRepository.AddToRoleAsync(createdUser, request.Role);
        
        // Ambil roles
        var roles = await UserRepository.GetRolesAsync(createdUser);
        
        // Mapping ke response
        var response = Mapper.Map<RegisterUserResponse>(createdUser);
        response.Roles = roles;
        
        return response;
    }
}