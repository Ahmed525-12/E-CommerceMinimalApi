using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Domain.Entities.AppEntitie;

public class OrderItem: BaseEntity
{
  [Required]
        public int OrderId { get; set; }
        
        [Required]
        public int ProductId { get; set; }
        
        public int? ProductVariantId { get; set; }
        
        [Required]
        public int Quantity { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal Subtotal { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal DiscountAmount { get; set; }
        
        // Navigation properties
        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }
        
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
        
        [ForeignKey("ProductVariantId")]
        public virtual ProductVariant ProductVariant { get; set; }
}
