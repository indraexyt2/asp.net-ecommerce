namespace Domain.Entity.Identity;

public static class Roles
{
    public const string Admin = "Admin";
    public const string Seller = "Seller";
    public const string Client = "Client";
        
    public static readonly string[] All = new[] { Admin, Seller, Client };
}