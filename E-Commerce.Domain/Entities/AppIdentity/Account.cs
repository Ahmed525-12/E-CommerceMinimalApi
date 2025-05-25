using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Entites.AppIdentity
{
    public class Account : IdentityUser
    {
        public string DisplayName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}