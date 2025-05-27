using System;
using System.Security.Claims;
using Carter;
using E_Commerce.Domain.DTOs.EmailDTO;
using E_Commerce.Domain.Entities.AppIdentity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Api.EndPoints.AuthAccount;

public class VerifyEmailEndPoint : CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/auth/verify-email", async (HttpContext httpContext, [FromServices] UserManager<Customer> userManager ,[FromBody] VerifyEmailReqDto req) =>
         {

             var emailClaim = httpContext.User.FindFirst(ClaimTypes.Email) ?? httpContext.User.FindFirst("email");

             if (emailClaim == null)
             {
                 return Results.BadRequest("Email claim not found in token.");
             }

             var email = emailClaim.Value;
var checkUser = await userManager.FindByEmailAsync(email);
             if (checkUser == null)
             {
                 return Results.BadRequest("User not found.");
             }

             // Check if the OTP is valid
             if (checkUser.OtpCode == null || checkUser.OtpExpiresAt == null || checkUser.OtpExpiresAt < DateTime.UtcNow)
             {
                 return Results.BadRequest("OTP is invalid or expired.");
             }

             // Verify the OTP
             var otp = req.OtpCode;
             if (otp != checkUser.OtpCode)
             {
                 return Results.BadRequest("Invalid OTP.");
             }

             // Mark the email as verified
             checkUser.EmailConfirmed = true;
             checkUser.OtpCode = null; // Clear the OTP after successful verification
             checkUser.OtpExpiresAt = null;

             var result = await userManager.UpdateAsync(checkUser);
             if (!result.Succeeded)
             {
                 return Results.BadRequest("Failed to update user.");
             }

             return Results.Ok("Email verified successfully.");




         })


        .WithName("VerifyEmail")
        .WithTags("Auth Account")
        .RequireAuthorization(new AuthorizeAttribute { Roles = "Customer" });
    }
}
