using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Domain.Entities.AppEntitie;

public class Category : BaseEntity
{
  [Required]
        [StringLength(100)]
        public string Name { get; set; }
        
        [StringLength(500)]
        public string Description { get; set; }
        
        
        [StringLength(200)]
        public string ImageUrl { get; set; }
        
        public bool IsActive { get; set; } = true;
        
    
        public virtual ICollection<Product> Products { get; set; }
}
