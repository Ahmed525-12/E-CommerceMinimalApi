using System;

namespace E_Commerce.Domain.DTOs.AuthDTOs;

public class RefreshTokenRequest
{
    public string RefreshToken { get; set; } = default!;

}
