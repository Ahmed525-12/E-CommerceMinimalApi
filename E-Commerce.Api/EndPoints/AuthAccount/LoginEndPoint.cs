using System;
using System.ComponentModel.DataAnnotations;
using Carter;
using E_Commerce.Domain.DTOs.AuthDTOs;
using E_Commerce.Domain.Entites.AppIdentity;
using E_Commerce.Handler.JWTToken.Intrefaces;
using E_Commerce.Handler.Wrapper.WorkWrapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Api.EndPoints.AuthAccount;

public class LoginEndPoint : CarterModule
{
  

   
  
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/auth/login", async ([FromBody] LoginDtoReq loginDtoReq, 
            UserManager<Account> userManager,
            SignInManager<Account> signInManager,
            ITokenService tokenService,
            ILogger<LoginEndPoint> logger) =>
         {
             try
             {
                 var validationResults = new List<ValidationResult>();
                 var validationContext = new ValidationContext(loginDtoReq);

                 bool isValid = Validator.TryValidateObject(loginDtoReq, validationContext, validationResults, true);

                 if (!isValid)
                 {

                     var errors = validationResults.Select(vr => vr.ErrorMessage).ToList();
                     var errorMessage = string.Join("; ", errors);

                     logger.LogWarning("Login validation failed: {Errors}", errorMessage);
                     return Results.BadRequest(Result<LoginDtoRes>.Fail(errorMessage));
                 }
                 var checkUser = await userManager.FindByEmailAsync(loginDtoReq.Email);
                 if (checkUser == null)
                 {
                     logger.LogWarning("Login failed: User with email {Email} not found", loginDtoReq.Email);
                     return Results.BadRequest(Result<LoginDtoRes>.Fail("User not found"));
                 }
                 var result = await signInManager.CheckPasswordSignInAsync(checkUser, loginDtoReq.Password, false);
                 if (!result.Succeeded)
                 {
                     logger.LogWarning("Login failed: Invalid credentials for user {Email}", loginDtoReq.Email);
                     return Results.BadRequest(Result<LoginDtoRes>.Fail("Invalid credentials"));
                 }
                 var token = await tokenService.CreateToken(checkUser);
                 var roles = await userManager.GetRolesAsync(checkUser);
                var refreshDto = await tokenService.CreateRefreshTokenAsync(checkUser);    
                 var loginDtoRes = new LoginDtoRes
                 {
                     Email = checkUser.Email,
                     DisplayName = checkUser.DisplayName,
                     Token = token,
                     RoleName = roles.FirstOrDefault(),
                    RefreshToken = refreshDto.Token
                 };

                 logger.LogInformation("User {Email} logged in successfully", loginDtoReq.Email);
                 return Results.Ok(Result<LoginDtoRes>.Success(loginDtoRes,"Login successful"));
             }
             catch (System.Exception)
             {

                 throw;
             }

         })
         .WithName("Login")
         .WithTags("Auth Account")
         .Accepts<LoginDtoReq>("application/json")
         .Produces<Result<LoginDtoRes>>(200)
         .Produces<Result<LoginDtoRes>>(400)
         .Produces<Result<LoginDtoRes>>(500)
         ;
    }
}
