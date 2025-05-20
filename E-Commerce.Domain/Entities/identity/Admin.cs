using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Entites.identity
{
    public class Admin : Account
    {
        [StringLength(50)]
        public string Department { get; set; }
        
        [StringLength(50)]
        public string Position { get; set; }
        
        public DateTime HireDate { get; set; }
        
        public int AccessLevel { get; set; } = 1;
    }
}