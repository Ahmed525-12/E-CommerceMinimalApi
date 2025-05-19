using Carter;
using E_Commerce.Domain.DTOs.EmailDTO;
using E_Commerce.Handler.ServicesExtension;
using E_Commerce.Infastrcture;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
builder.Services.AddAplicationServices();
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();
builder.Host.UseSerilog((context, configuration) => 
    configuration.ReadFrom.Configuration(context.Configuration));
builder.Services.AddDbContext<AppDbContexts>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddOpenApi();
builder.Services.AddCarter();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();

}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapCarter();
using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var roles = new[] {"Costumer", "Admin" };
                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }
            }

app.Run();


