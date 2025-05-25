using Carter;
using E_Commerce.Domain.DTOs.EmailDTO;
using E_Commerce.Domain.Entites.AppIdentity;
using E_Commerce.Domain.Entities.AppIdentity;
using E_Commerce.Handler.ServicesExtension;
using E_Commerce.Infastrcture;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
// Configure mail settings
builder.Services.Configure<MailSettings>(
    builder.Configuration.GetSection("MailSettings"));

// Add database context
builder.Services.AddDbContext<AppDbContexts>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure Identity with a single user type (Account) and roles
 builder.Services.AddDefaultIdentity<Account>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
            })
            .AddRoles<IdentityRole>() // Add roles
            .AddDefaultTokenProviders()
            .AddEntityFrameworkStores<AppDbContexts>()

            ;

            builder.Services.AddIdentityCore<Customer>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
            }).AddRoles<IdentityRole>() // Add roles
            .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<AppDbContexts>()
                .AddSignInManager<SignInManager<Customer>>()
                ;

             builder.Services.AddIdentityCore<Admin>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
            }).AddRoles<IdentityRole>() // Add roles
            .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<AppDbContexts>()
                .AddSignInManager<SignInManager<Admin>>()
                ;     

// Add JWT authentication and other application services
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddAplicationServices();

// Configure Serilog
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

// Add OpenAPI (Swagger) and Carter
builder.Services.AddOpenApi();
builder.Services.AddCarter();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();

}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

// Map Carter endpoints
app.MapCarter();

// Seed roles
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var roles = new[] { "Customer", "Admin" };
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}

app.Run();
