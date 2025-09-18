using Microsoft.AspNetCore.Identity;

namespace Carlos_Pizza.Data;

public static class IdentitySeedData
{
    public static async Task Initialize(
        CarlosDB context,
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager, 
        IConfiguration configuration
    )
    {

        // we will add admin and member roles, using
        string adminRole = "Admin";
        string memberRole = "Member";
        string adminEmail = configuration["Admin:Email"]
                            ?? throw new InvalidOperationException("Admin:Email must be set.");
        string adminPassword = configuration["Admin:Password"]
                               ?? throw new InvalidOperationException("Admin:Password must be set.");
        bool createDemoUser = bool.TryParse(configuration["Enable:DemoUser"], out var result) && result;

        


        // look for an existing admin role, if not found then create it
        if (await roleManager.FindByNameAsync(adminRole) == null)
        {
            await roleManager.CreateAsync(new IdentityRole(adminRole));
        }
        
        // look for an existing member role, if not found then create it
        if (await roleManager.FindByNameAsync(memberRole) == null)
        {
            await roleManager.CreateAsync(new IdentityRole(memberRole));
        }
        
        
        // Look for an admin user, if not exist then create with these details
        if (await userManager.FindByNameAsync(adminEmail) == null)
        {
            var user = new IdentityUser
            {
                UserName = adminEmail,
                Email = adminEmail,
            };
            //if user created then add them to the admin role
            var adminCreationResult = await userManager.CreateAsync(user);
            if (adminCreationResult.Succeeded)
            {
                await userManager.AddPasswordAsync(user, adminPassword);
                await userManager.AddToRoleAsync(user, adminRole);
            }
        }
        // Look for a member user, if not exist then create with these details
        if (createDemoUser && await userManager.FindByNameAsync("me@example.com") == null)
        {
            var user = new IdentityUser
            {
                UserName = "me@example.com",
                Email = "me@example.com",
                PhoneNumber = "06124 648200"
            };
            //if user created then add them to the admin role
            var demoCreationResult = await userManager.CreateAsync(user);
            if (demoCreationResult.Succeeded)
            {
                await userManager.AddPasswordAsync(user, "password123");
                await userManager.AddToRoleAsync(user, memberRole);
            }
        }
    }
}