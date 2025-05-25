using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using E_Commerce.Domain.Entities.AppIdentity;

namespace E_Commerce.Domain.Entities.AppEntitie;

public class Review : BaseEntity
{
[Required]
        public int ProductId { get; set; }
        
        [Required]
        public string UserId { get; set; }
        
        [Required]
        public int Rating { get; set; } // 1-5 stars
        
        [StringLength(1000)]
        public string Comment { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public DateTime? UpdatedAt { get; set; }
        
        public bool IsVerifiedPurchase { get; set; } = false;
        
        public bool IsApproved { get; set; } = false;
        
        // Navigation properties
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
        
        [ForeignKey("UserId")]
        public virtual Customer Customer { get; set; }
}
