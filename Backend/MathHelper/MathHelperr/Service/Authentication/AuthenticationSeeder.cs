
using Microsoft.AspNetCore.Identity;

namespace MathHelperr.Service.Authentication;

public class AuthenticationSeeder
{
   
    private RoleManager<IdentityRole> roleManager;
    private UserManager<IdentityUser> userManager;
    private readonly IConfiguration _configuration;

    public AuthenticationSeeder(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager, IConfiguration configuration)
    {
        this.roleManager = roleManager;
        this.userManager = userManager;
        _configuration = configuration;
    }
    
    public void AddRoles()
    {
        var tAdmin = CreateAdminRole(roleManager);
        tAdmin.Wait();

        var tUser = CreateUserRole(roleManager);
        tUser.Wait();
    }
    
    private async Task CreateAdminRole(RoleManager<IdentityRole> roleManager)
    {
        await roleManager.CreateAsync(new IdentityRole("Admin")); //The role string should better be stored as a constant or a value in appsettings
    }

    async Task CreateUserRole(RoleManager<IdentityRole> roleManager)
    {
        await roleManager.CreateAsync(new IdentityRole("User")); //The role string should better be stored as a constant or a value in appsettings
    }
    
    public void AddAdmin()
    {
        var tAdmin = CreateAdminIfNotExists();
        tAdmin.Wait();
    }

    private async Task CreateAdminIfNotExists()
    {
        var email = _configuration["ADMIN_EMAIL"];
        var password =_configuration["ADMIN_PASSWORD"];
        Console.WriteLine($"Admin Email: {email}");
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            throw new Exception("ADMIN_EMAIL or ADMIN_PASSWORD environment variable is missing.");
        }
        var adminInDb = await userManager.FindByEmailAsync(email);
        if (adminInDb == null)
        {
            var admin = new IdentityUser { UserName = "admin", Email = email };
            var adminCreated = await userManager.CreateAsync(admin, password);

            if (adminCreated.Succeeded)
            {
                await userManager.AddToRoleAsync(admin, "Admin");
            }
        }
    }
}