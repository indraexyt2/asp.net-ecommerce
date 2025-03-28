using Domain.Entity.Identity;

namespace Application.Repositories;

public interface IUserRepository 
{
    Task<AppUser?> GetByIdAsync(string id);
    Task<AppUser?> GetByUsernameAsync(string username);
    Task<AppUser?> GetByEmailAsync(string email);
    Task<bool> CheckPasswordAsync(AppUser user, string password);
    Task<AppUser> CreateUserAsync(AppUser user, string password);
    Task<bool> IsInRoleAsync(AppUser user, string role);
    Task AddToRoleAsync(AppUser user, string role);
    Task<List<string>> GetRolesAsync(AppUser user);
}