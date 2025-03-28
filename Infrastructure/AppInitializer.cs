using Domain.Entity.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Infrastructure;

public class AppInitializer
{
     public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var services = scope.ServiceProvider;
        var logger = services.GetRequiredService<ILogger<IdentityRole>>();

        try
        {
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = services.GetRequiredService<UserManager<AppUser>>();

            await SeedRolesAsync(roleManager);
            await SeedAdminUserAsync(userManager);
            
            logger.LogInformation("Seeding database completed successfully.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while seeding the database.");
        }
    }

    private static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
    {
        foreach (var role in Roles.All)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }

    private static async Task SeedAdminUserAsync(UserManager<AppUser> userManager)
    {
        // Buat user admin default jika belum ada
        var adminEmail = "admin@example.com";
        var adminUser = await userManager.FindByEmailAsync(adminEmail);

        if (adminUser == null)
        {
            adminUser = new AppUser
            {
                UserName = "admin",
                Email = adminEmail,
                FirstName = "Admin",
                LastName = "User",
                EmailConfirmed = true,
                DateCreated = DateTimeOffset.UtcNow
            };

            var result = await userManager.CreateAsync(adminUser, "Admin@123");
            
            if (result.Succeeded)
            {
                // Tambahkan ke role Admin
                await userManager.AddToRoleAsync(adminUser, Roles.Admin);
            }
        }
        
        // Buat user seller default
        var sellerEmail = "seller@example.com";
        var sellerUser = await userManager.FindByEmailAsync(sellerEmail);

        if (sellerUser == null)
        {
            sellerUser = new AppUser
            {
                UserName = "seller",
                Email = sellerEmail,
                FirstName = "Seller",
                LastName = "User",
                EmailConfirmed = true,
                DateCreated = DateTimeOffset.UtcNow
            };

            var result = await userManager.CreateAsync(sellerUser, "Seller@123");
            
            if (result.Succeeded)
            {
                // Tambahkan ke role Seller
                await userManager.AddToRoleAsync(sellerUser, Roles.Seller);
            }
        }
        
        // Buat user client default
        var clientEmail = "client@example.com";
        var clientUser = await userManager.FindByEmailAsync(clientEmail);

        if (clientUser == null)
        {
            clientUser = new AppUser
            {
                UserName = "client",
                Email = clientEmail,
                FirstName = "Client",
                LastName = "User",
                EmailConfirmed = true,
                DateCreated = DateTimeOffset.UtcNow
            };

            var result = await userManager.CreateAsync(clientUser, "Client@123");
            
            if (result.Succeeded)
            {
                // Tambahkan ke role Client
                await userManager.AddToRoleAsync(clientUser, Roles.Client);
            }
        }
    }
}