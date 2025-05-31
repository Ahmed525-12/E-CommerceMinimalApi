using System;

namespace E_Commerce.Domain.DTOs.CategoryDTOs;

public class CategoryRes
{
public int Id { get; set; }
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public string ImageUrl { get; set; }
        
        public bool IsActive { get; set; } = true;
}
