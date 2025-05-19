using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.DTOs.EmailDTO
{
    public class RecieveEmail
    {
        public string token { get; set; }
        public string email { get; set; }
    }
}