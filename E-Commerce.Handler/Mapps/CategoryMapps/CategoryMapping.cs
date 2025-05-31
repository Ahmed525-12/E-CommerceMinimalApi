using System;
using E_Commerce.Domain.DTOs.CategoryDTOs;
using E_Commerce.Domain.Entities.AppEntitie;

namespace E_Commerce.Handler.Mapps.CategoryMapps;

public static class CategoryMapping
{

    public static Category MapCategory(CategoryCreateReq category)
    {


        return new Category
        {
            Name = category.Name,
            Description = category.Description,
            ImageUrl = category.ImageUrl,
            IsActive = category.IsActive
        };
    }

    public static CategoryRes MapCategoryRes(Category category)
    {


        return new CategoryRes
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
            ImageUrl = category.ImageUrl,
            IsActive = category.IsActive
        };
    } 
    
   public static IEnumerable<CategoryRes> MapCategoryResList(
    IEnumerable<Category> categories)
{
    return categories.Select(category => new CategoryRes
    {
        Id = category.Id,
        Name = category.Name,
        Description = category.Description,
        ImageUrl = category.ImageUrl,
        IsActive = category.IsActive
    });
}

}

