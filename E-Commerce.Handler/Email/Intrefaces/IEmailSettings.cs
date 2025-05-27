using E_Commerce.Domain.DTOs.EmailDTO;
using ElmnasaDomain.DTOs.EmailDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Handler.Email.Intrefaces
{
    public interface IEmailSettings
    {
        public void SendEmail(EmailDTO email);
    }

  
}