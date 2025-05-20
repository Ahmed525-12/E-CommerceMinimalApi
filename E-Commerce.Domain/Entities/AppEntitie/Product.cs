using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Domain.Entities.AppEntitie;

public class Product: BaseEntity
{
  [Required]
        [StringLength(200)]
        public string Name { get; set; }
        
        [StringLength(2000)]
        public string Description { get; set; }
        
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal? DiscountPrice { get; set; }
        
        public int StockQuantity { get; set; }
        
        [Required]
        public int CategoryId { get; set; }
        
        [StringLength(50)]
        public string SKU { get; set; }
        
        public float Weight { get; set; }
        
        [StringLength(50)]
        public string Dimensions { get; set; }
        
        public bool IsFeatured { get; set; } = false;
        
        public bool IsActive { get; set; } = true;
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public DateTime? UpdatedAt { get; set; }
        
        // Navigation properties
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
        public virtual ICollection<ProductImage> Images { get; set; }
        public virtual ICollection<ProductVariant> Variants { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<WishlistItem> WishlistItems { get; set; }
}
