using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Domain.Entities.AppEntitie;

public class ProductVariant: BaseEntity
{
 [Required]
        public int ProductId { get; set; }
        
        [Required]
        [StringLength(100)]
        public string VariantName { get; set; }
        
        [StringLength(50)]
        public string SKU { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal? AdditionalPrice { get; set; }
        
        public int StockQuantity { get; set; }
        
        // Navigation property
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
}
