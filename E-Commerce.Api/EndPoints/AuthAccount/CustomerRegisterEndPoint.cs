using System;
using System.ComponentModel.DataAnnotations;
using Carter;
using E_Commerce.Domain.DTOs.AuthDTOs;
using E_Commerce.Domain.Entites.AppIdentity;
using E_Commerce.Domain.Entities.AppIdentity;
using E_Commerce.Handler.JWTToken.Intrefaces;
using E_Commerce.Handler.Mapps.AuthMapps;
using E_Commerce.Handler.Wrapper.WorkWrapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Api.EndPoints.AuthAccount;

public class CustomerRegisterEndPoint : CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/auth/CustomerRegister", async (
        [FromServices] ITokenService tokenService,
        [FromServices] UserManager<Customer> userManager,
        [FromServices] ILogger<CustomerRegisterEndPoint> logger,
        [FromBody] CustomerRegistoerDtoReq registerDtoReq
    ) =>
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(registerDtoReq);

            bool isValid = Validator.TryValidateObject(registerDtoReq, validationContext, validationResults, true);

            if (!isValid)
            {

                var errors = validationResults.Select(vr => vr.ErrorMessage).ToList();
                var errorMessage = string.Join("; ", errors);

                logger.LogWarning("Register validation failed: {Errors}", errorMessage);
                return Results.BadRequest(Result<CustomerRegistoerDtoRes>.Fail(errorMessage));
            }
            var checkUser = await userManager.FindByEmailAsync(registerDtoReq.Email);
            if (checkUser != null)
            {
                logger.LogWarning("Register failed: User with email {Email} is Exist", registerDtoReq.Email);
                return Results.BadRequest(Result<CustomerRegistoerDtoRes>.Fail("User is Exist"));
            }
            var account = AuthMapping.ToAccountFromRegisterReq(registerDtoReq);

            // Set default value for DefaultBillingAddress
            account.DefaultBillingAddress = "Default Address";

            var result = await userManager.CreateAsync(account, registerDtoReq.Password);
            if (!result.Succeeded)
            {
                logger.LogWarning("Register failed: {Errors}", string.Join(", ", result.Errors.Select(e => e.Description)));
                return Results.BadRequest(Result<CustomerRegistoerDtoRes>.Fail(string.Join(", ", result.Errors.Select(e => e.Description))));
            }
            var roleResult = await userManager.AddToRoleAsync(account, "Customer");
            if (!roleResult.Succeeded)
            {
                logger.LogWarning("Failed to add role 'Customer' to user {Email}: {Errors}", registerDtoReq.Email, string.Join(", ", roleResult.Errors.Select(e => e.Description)));
                return Results.BadRequest(Result<CustomerRegistoerDtoRes>.Fail("Failed to assign role to user."));
            }

            // Fetch the role name to include in the response
            var roleName = (await userManager.GetRolesAsync(account)).FirstOrDefault();

            var token = await tokenService.CreateToken(account);
            var registerDtoRes = new CustomerRegistoerDtoRes
            {
                Email = account.Email,
                DisplayName = account.DisplayName,
                Token = token,
                RoleName = roleName
            };
            logger.LogInformation("User {Email} registered successfully", registerDtoReq.Email);
            return Results.Ok(Result<CustomerRegistoerDtoRes>.Success(registerDtoRes, "User registered successfully"));

        })
 .WithName("CustomerRegister")
         .WithTags("Auth Account")
         .Accepts<CustomerRegistoerDtoReq>("application/json")
         .Produces<Result<CustomerRegistoerDtoRes>>(200)
         .Produces<Result<CustomerRegistoerDtoRes>>(400)
         .Produces<Result<CustomerRegistoerDtoRes>>(500)

        ;
    }
}
