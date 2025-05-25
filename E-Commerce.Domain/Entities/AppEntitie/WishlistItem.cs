using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using E_Commerce.Domain.Entities.AppIdentity;

namespace E_Commerce.Domain.Entities.AppEntitie;

public class WishlistItem: BaseEntity
{
[Required]
        public string UserId { get; set; }
        
        [Required]
        public int ProductId { get; set; }
        
        public DateTime AddedAt { get; set; } = DateTime.UtcNow;
        
        [StringLength(255)]
        public string Notes { get; set; }
        
        // Navigation properties
        [ForeignKey("UserId")]
        public virtual Customer Customer { get; set; }
        
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
}
