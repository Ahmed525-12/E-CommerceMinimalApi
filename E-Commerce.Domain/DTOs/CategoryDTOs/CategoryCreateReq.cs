using System;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Domain.DTOs.CategoryDTOs;

public class CategoryCreateReq
{
[Required]
        [StringLength(100)]
        public string Name { get; set; }
        
        [StringLength(500)]
        public string Description { get; set; }
        
        
        [StringLength(200)]
        public string ImageUrl { get; set; }
        
        public bool IsActive { get; set; } = true;
}
