using System;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Domain.Entities.AppEntitie;

public class Coupon : BaseEntity
{
[Required]
        [StringLength(50)]
        public string Code { get; set; }
        
        [StringLength(200)]
        public string Description { get; set; }
        
        [Required]
        public decimal DiscountAmount { get; set; }
        
        public bool IsPercentage { get; set; } = true;
        
        public decimal? MinimumOrderAmount { get; set; }
        
        public int? MaxUsage { get; set; }
        
        public int UsageCount { get; set; } = 0;
        
        public DateTime StartDate { get; set; }
        
        public DateTime? EndDate { get; set; }
        
        public bool IsActive { get; set; } = true;
}
