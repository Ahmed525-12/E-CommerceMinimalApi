using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Domain.Entities.AppEntitie;

public class OrderStatusHistory: BaseEntity
{
 [Required]
        public int OrderId { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Status { get; set; }
        
        [StringLength(500)]
        public string Comments { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        [StringLength(100)]
        public string UpdatedBy { get; set; }
        
        // Navigation property
        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }
}
