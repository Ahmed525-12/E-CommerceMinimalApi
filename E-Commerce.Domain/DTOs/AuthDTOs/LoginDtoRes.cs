using System;

namespace E_Commerce.Domain.DTOs.AuthDTOs;

public class LoginDtoRes
{
     public string Email { get; set; }

        public string DisplayName { get; set; }
        public string Token { get; set; }
        public List<string> RoleName { get; set; }
}
