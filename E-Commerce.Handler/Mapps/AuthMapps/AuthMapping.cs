using System;
using E_Commerce.Domain.DTOs.AuthDTOs;
using E_Commerce.Domain.Entites.AppIdentity;
using E_Commerce.Domain.Entities.AppIdentity;

namespace E_Commerce.Handler.Mapps.AuthMapps;

public static class AuthMapping
{
    public static Customer ToAccountFromRegisterReq(this CustomerRegistoerDtoReq req)
    {
        

        return new Customer
        {
            Email = req.Email,
            DisplayName = req.DisplayName,
            UserName=  req.Email.Split('@')[0],
        };
    }
}
