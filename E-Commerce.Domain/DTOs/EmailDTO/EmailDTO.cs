using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElmnasaDomain.DTOs.EmailDTO
{
    public class EmailDTO
    {
        public int Id { get; set; } = 1;

       public string To { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
    public string HtmlBody { get; set; } // Add this property for HTML content
    public bool IsHtml { get; set; } = false; // Flag to indicate content type
    }
}