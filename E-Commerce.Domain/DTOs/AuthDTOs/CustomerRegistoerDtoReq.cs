using System;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Domain.DTOs.AuthDTOs;

public class CustomerRegistoerDtoReq
{
        [Required(ErrorMessage = "Email is requird")]
        [EmailAddress(ErrorMessage = "Email invalid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "name is requird")]
        public string DisplayName { get; set; }

        [Required(ErrorMessage = "password is requird")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
}
