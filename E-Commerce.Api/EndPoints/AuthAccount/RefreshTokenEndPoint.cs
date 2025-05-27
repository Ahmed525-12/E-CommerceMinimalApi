using System;
using Carter;
using E_Commerce.Domain.DTOs.AuthDTOs;
using E_Commerce.Handler.JWTToken.Intrefaces;
using E_Commerce.Handler.Wrapper.WorkWrapper;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Api.EndPoints.AuthAccount;

public class RefreshTokenEndPoint : CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
       app.MapPost("/api/auth/refresh-token", async ([FromBody] RefreshTokenRequest refreshToken, ITokenService tokenService) =>
        {
            if (string.IsNullOrEmpty(refreshToken.RefreshToken))
            {
                return Results.BadRequest(Result<RefreshTokenRes>.Fail("Refresh token is required."));
            }

            try
            {
                var result = await tokenService.RefreshTokenAsync(refreshToken.RefreshToken);
                return Results.Ok(result);
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        })
        .WithName("RefreshToken")
        .WithTags("Auth Account");
    }
}
