using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using E_Commerce.Domain.Entities.identity;

namespace E_Commerce.Domain.Entities.AppEntitie;

public class Order : BaseEntity
{
  [Required]
        public string UserId { get; set; }
        
        [Required]
        [StringLength(50)]
        public string OrderNumber { get; set; }
        
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal ShippingAmount { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal TaxAmount { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal DiscountAmount { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Status { get; set; } = "Pending";
        
        [StringLength(50)]
        public string PaymentStatus { get; set; } = "Pending";
        
        [StringLength(50)]
        public string PaymentMethod { get; set; }
        
        public int? ShippingAddressId { get; set; }
        
        public int? BillingAddressId { get; set; }
        
        [StringLength(500)]
        public string Notes { get; set; }
        
        // Navigation properties
        [ForeignKey("UserId")]
        public virtual Customer Customer { get; set; }
        
        [ForeignKey("ShippingAddressId")]
        public virtual Address ShippingAddress { get; set; }
        
        [ForeignKey("BillingAddressId")]
        public virtual Address BillingAddress { get; set; }
        
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<OrderStatusHistory> StatusHistory { get; set; }
}
