using Microsoft.AspNetCore.Identity;

namespace Carlos_Pizza.Data;

public static class IdentitySeedData
{
    public static async Task Initialize(CarlosDB context,
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager
    )
    {
        // this is an EF core method to check if the database has been created
        // if it does no action is taken, if not the database and all its schema are created
        context.Database.EnsureCreated();

        // we will add admin and member roles, using
        string adminRole = "Admin";
        string memberRole = "Member";
        string adminEmail = Environment.GetEnvironmentVariable("ADMIN_EMAIL") 
                            ?? throw new InvalidOperationException("ADMIN_EMAIL must be set in environment.");
        string adminPassword = Environment.GetEnvironmentVariable("ADMIN_PASSWORD") 
                               ?? throw new InvalidOperationException("ADMIN_PASSWORD must be set in environment.");
        bool createDemoUser = bool.TryParse(Environment.GetEnvironmentVariable("CREATE_DEMO_USER"), out var result) && result;
        


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