using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using E_Commerce.Domain.Entities.AppIdentity;

namespace E_Commerce.Domain.Entities.AppEntitie;

public class Cart:  BaseEntity
{
   [Required]
        public string UserId { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; } = 0;
        
        // Navigation properties
        [ForeignKey("UserId")]
        public virtual Customer Customer { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }
}
