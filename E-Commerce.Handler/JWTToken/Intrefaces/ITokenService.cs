using E_Commerce.Domain.DTOs.AuthDTOs;
using E_Commerce.Domain.Entites.AppIdentity;
using E_Commerce.Domain.Entities.AppIdentity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Handler.JWTToken.Intrefaces
{
    public interface ITokenService
    {
        public Task<string> CreateToken(Account user);
        public Task<(string Token, DateTime Expiry)> CreateRefreshTokenAsync(Account user);
        public  Task<RefreshTokenRes> RefreshTokenAsync(string request);
    }
}