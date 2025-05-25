using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using E_Commerce.Domain.Entites.AppIdentity;
using E_Commerce.Domain.Entities.AppEntitie;

namespace E_Commerce.Domain.Entities.AppIdentity;

public class Customer: Account
{
 public DateTime? DateOfBirth { get; set; }
        
        [StringLength(100)]
        public string? DefaultShippingAddress { get; set; }
        
        [StringLength(100)]
        public string DefaultBillingAddress { get; set; }
        
        public bool SubscribedToNewsletter { get; set; } = false;
        
        public int LoyaltyPoints { get; set; } = 0;
        
        public string CustomerTier { get; set; } = "Standard";
        
       
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<WishlistItem> WishlistItems { get; set; }
        public virtual Cart Cart { get; set; }
}
