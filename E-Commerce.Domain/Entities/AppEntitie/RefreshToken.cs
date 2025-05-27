using System;
using E_Commerce.Domain.Entites.AppIdentity;
using E_Commerce.Domain.Entities.AppIdentity;

namespace E_Commerce.Domain.Entities.AppEntitie;

public class RefreshToken : BaseEntity
{
 public string Token { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public DateTime Expires { get; set; }
    public Account User { get; set; }
}
