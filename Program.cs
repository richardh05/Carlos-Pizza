using Carlos_Pizza.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
var builder = WebApplication.CreateBuilder(args);

// Created with help from the Microsoft Learn documentation (Dykstra et al., 2024)
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<CarlosDB>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CarlosDB") ?? throw new InvalidOperationException("Connection string 'CarlosDB' not found.")));

// commenting out default identity to replace it with one that adds Identity AND IdentityRole
//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<CarlosDB>();

builder.Services.AddIdentity<IdentityUser, IdentityRole>(
    options => options.Stores.MaxLengthForKeys = 128
    )
        .AddEntityFrameworkStores<CarlosDB>()
        .AddRoles<IdentityRole>()
        .AddDefaultUI()
        .AddDefaultTokenProviders();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
    
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<CarlosDB>();
    
    int maxRetries = 10;
    int retryDelay = 5000; // 5 seconds
    
    for (int i = 0; i < maxRetries; i++)
    {
        try
        {
            Console.WriteLine("Attempting database migration...");
            context.Database.Migrate();
            Console.WriteLine("Database migration successful!");
            // If migration is successful, break the loop
            break;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Migration failed on attempt {i + 1} of {maxRetries}. Retrying in {retryDelay / 1000} seconds. Error: {ex.Message}");
            Thread.Sleep(retryDelay);
            if (i == maxRetries - 1)
            {
                Console.WriteLine("Maximum migration retries exhausted. The application will now terminate.");
                throw; // Throw the exception if all retries fail
            }
        }
    }
    
    // seed app-specific data
    DbInitializer.Initialize(context);

    // seed identity users & roles
    var userMgr = services.GetRequiredService<UserManager<IdentityUser>>();
    var roleMgr = services.GetRequiredService<RoleManager<IdentityRole>>();
    var config = services.GetRequiredService<IConfiguration>();

    IdentitySeedData.Initialize(context, userMgr, roleMgr, config).Wait();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();

// VVV Always keep at the bottom, makes the app run
app.Run();
// end of adapted code