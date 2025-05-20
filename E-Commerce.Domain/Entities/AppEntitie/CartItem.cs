using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Domain.Entities.AppEntitie;

public class CartItem : BaseEntity
{
 [Required]
        public int CartId { get; set; }
        
        [Required]
        public int ProductId { get; set; }
        
        public int? ProductVariantId { get; set; }
        
        [Required]
        public int Quantity { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }
        
        public DateTime AddedAt { get; set; } = DateTime.UtcNow;
        
        // Navigation properties
        [ForeignKey("CartId")]
        public virtual Cart Cart { get; set; }
        
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
        
        [ForeignKey("ProductVariantId")]
        public virtual ProductVariant ProductVariant { get; set; }
}
