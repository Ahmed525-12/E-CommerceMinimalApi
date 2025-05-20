using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Domain.Entities.AppEntitie;

public class ProductImage : BaseEntity
{
  [Required]
        public int ProductId { get; set; }
        
        [Required]
        [StringLength(500)]
        public string ImageUrl { get; set; }
        
        [StringLength(100)]
        public string AltText { get; set; }
        
        public bool IsPrimary { get; set; } = false;
        
        public int DisplayOrder { get; set; } = 0;
        
        // Navigation property
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
}
