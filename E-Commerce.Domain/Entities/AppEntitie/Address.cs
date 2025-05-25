using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using E_Commerce.Domain.Entities.AppIdentity;

namespace E_Commerce.Domain.Entities.AppEntitie;

public class Address : BaseEntity
{
 [Required]
        public string UserId { get; set; }
        
        [Required]
        [StringLength(100)]
        public string FullName { get; set; }
        
        [Required]
        [StringLength(200)]
        public string AddressLine1 { get; set; }
        
        [StringLength(200)]
        public string AddressLine2 { get; set; }
        
        [Required]
        [StringLength(100)]
        public string City { get; set; }
        
        [Required]
        [StringLength(100)]
        public string State { get; set; }
        
        [Required]
        [StringLength(20)]
        public string ZipCode { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Country { get; set; }
        
        [StringLength(20)]
        public string PhoneNumber { get; set; }
        
        public bool IsDefault { get; set; } = false;
        
        [StringLength(50)]
        public string AddressType { get; set; } = "Both"; // Shipping, Billing, or Both
        
        // Navigation property
        [ForeignKey("UserId")]
        public virtual Customer Customer { get; set; }
}
