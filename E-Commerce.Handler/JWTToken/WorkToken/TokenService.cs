using E_Commerce.Domain.DTOs.AuthDTOs;
using E_Commerce.Domain.Entites.AppIdentity;
using E_Commerce.Domain.Entities.AppEntitie;
using E_Commerce.Domain.Entities.AppIdentity;
using E_Commerce.Handler.JWTToken.Intrefaces;
using E_Commerce.Infastrcture;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ZLinq;

namespace E_Commerce.Handler.JWTToken.WorkToken
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<Account> _accountManager;
        private readonly AppDbContexts _db;
        private readonly ILogger<TokenService> _logger;

        public TokenService(IConfiguration configuration, UserManager<Account> accountManager , AppDbContexts db,ILogger<TokenService> logger)
        {
            _configuration = configuration;
            _accountManager = accountManager;
            _db = db;
            _logger = logger;
        }

        public async Task<string> CreateToken(Account user)
        {
            // Fetch user roles from the account manager
            var roles = await _accountManager.GetRolesAsync(user);
            // Claims
            var UserClaim = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.DisplayName),
            };
            // Add role claims
            if (roles != null)
            {
                foreach (var role in roles)
                {
                    UserClaim.Add(new Claim(ClaimTypes.Role, role));
                }
            }

            // Security Key
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));

            // Create Token Object
            var Token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                expires: DateTime.Now.AddDays(double.Parse(_configuration["JWT:Expire"])),
                claims: UserClaim,
                signingCredentials: new SigningCredentials(Key, SecurityAlgorithms.HmacSha256Signature)
                );

            return new JwtSecurityTokenHandler().WriteToken(Token);
        }
          /// <summary>
    /// Generates a cryptographically secure random token
    /// </summary>
    private string GenerateRefreshToken()
    {
        var randomBytes = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);
        return Convert.ToBase64String(randomBytes);
    }



    /// <summary>
    /// Creates a new refresh token for a user
    /// </summary>
    public async Task<(string Token, DateTime Expiry)> CreateRefreshTokenAsync(Account user)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));

        var token = GenerateRefreshToken();


        var refreshToken = new RefreshToken
        {
            Token = token,
            UserId = user.Id,
            Expires = DateTime.UtcNow.AddDays(7),
            User = user
        };

        try
        {
            await _db.RefreshToken.AddAsync(refreshToken);
            await _db.SaveChangesAsync();

            _logger.LogInformation("Created new refresh token for user {UserId}", user.Id);

            return (token, refreshToken.Expires);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create refresh token for user {UserId}", user.Id);
            throw new InvalidOperationException("Failed to create refresh token", ex);
        }
    }
    public async Task<RefreshTokenRes?> RefreshTokenAsync(string request)
{
    // Validate input
    if (string.IsNullOrWhiteSpace(request))
    {
        _logger.LogWarning("Refresh token request is null or empty");
        throw new ArgumentNullException(nameof(request), "Refresh token cannot be null or empty");
    }

    try
    {
     
            var result = await _db.RefreshToken
                .Where(rt => rt.Token == request)
                .Select(rt => new
                {
                    Entity = rt,
                    User = rt.User
                })
                .FirstOrDefaultAsync();

            var refreshToken = result?.Entity;
            if (refreshToken != null)
            {
                refreshToken.User = result.User;
                
                
                refreshToken = new[] { refreshToken }
                    .AsValueEnumerable()
                    .First();
            }


        // Check if token exists
        if (refreshToken == null)
        {
            _logger.LogWarning("Refresh token not found: {Token}", request);
            throw new ApplicationException("Invalid refresh token");
        }

        // Check if token is expired
        if (refreshToken.Expires < DateTime.UtcNow)
        {
            _logger.LogWarning("Refresh token expired for user: {UserId}", refreshToken.User?.Id);
            
            // Remove expired token from database
            _db.RefreshToken.Remove(refreshToken);
            await _db.SaveChangesAsync();
            
            throw new ApplicationException("Refresh token expired");
        }

        // Check if user is still active/valid
        if (refreshToken.User == null)
        {
            _logger.LogWarning("User not found for refresh token");
            throw new ApplicationException("Invalid user associated with refresh token");
        }

        // Generate new access token
        var accessToken = await CreateToken(refreshToken.User);
        
        // Generate new refresh token
        var newRefreshToken = GenerateRefreshToken();
        
        // Update refresh token in database
        refreshToken.Token = newRefreshToken;
        refreshToken.Expires = DateTime.UtcNow.AddDays(7);
   

        // Save changes to database
        await _db.SaveChangesAsync();

        _logger.LogInformation("Successfully refreshed token for user: {UserId}", refreshToken.User.Id);

        return new RefreshTokenRes
        {
            Token = accessToken,
            RefreshToken = newRefreshToken
        };
    }
 
    catch (Exception ex)
    {
        _logger.LogError(ex, "Unexpected error occurred while refreshing token");
        throw new ApplicationException("An error occurred while processing the refresh token");
    }
}

       
    }
}