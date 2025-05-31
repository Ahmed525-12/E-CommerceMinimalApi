using System;
using E_Commerce.Domain.DTOs.CategoryDTOs;
using E_Commerce.Handler.HandlerServices.InterfacesServices;
using E_Commerce.Handler.Mapps.CategoryMapps;
using E_Commerce.Handler.Wrapper.WorkWrapper;
using E_Commerce.Infastrcture;
using Microsoft.EntityFrameworkCore;
using ZLinq;

namespace E_Commerce.Handler.HandlerServices.WorkServices;

public class CategoryService : ICategoryService
{
    private readonly AppDbContexts _appDbContext;

    public CategoryService(AppDbContexts appDbContext)
    {
        _appDbContext = appDbContext;
    }
    public async Task<CategoryRes> CreateCategoryAsync(CategoryCreateReq category)
    {
        var mapCategory = CategoryMapping.MapCategory(category);
        var addCategory = await _appDbContext.Categories.AddAsync(mapCategory);
        if (addCategory == null)
        {
            Result<CategoryRes>.Fail("Category creation failed.");

        }

        await _appDbContext.SaveChangesAsync();
        var mappedCategoryRes = CategoryMapping.MapCategoryRes(addCategory.Entity);
        return mappedCategoryRes;
       
    }

    public async Task<bool> DeleteCategoryAsync(int id)
    {
        var category = _appDbContext.Categories.Find(id);
        if (category == null)
        {
            return false;
        }
        
        var removeCategory= _appDbContext.Categories.Remove(category);
        if (removeCategory == null)
        {
            return false;
        }
        await _appDbContext.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<CategoryRes>> GetAllCategoriesAsync()
    {
        var categories = await _appDbContext.Categories.ToListAsync();
        var categoryResponses = categories
            .AsValueEnumerable()
            .Select(CategoryMapping.MapCategoryRes)
            .ToList();
        return categoryResponses;         
    
    }

    public async Task<CategoryRes> GetCategoryByIdAsync(int id)
    {
        var category = await _appDbContext.Categories.FindAsync(id);
        
        if (category == null)
        {
            return null;
        }
        
        var categoryResponse = CategoryMapping.MapCategoryRes(category);
        return categoryResponse;
        
    }


    public Task<CategoryRes> UpdateCategoryAsync(int id ,CategoryCreateReq category)
    {
        var existingCategory = _appDbContext.Categories.Find(id);
        if (existingCategory == null)
        {
            return Task.FromResult<CategoryRes>(null);
        }

        existingCategory.Name = category.Name;
        existingCategory.Description = category.Description;
        existingCategory.ImageUrl = category.ImageUrl;

        _appDbContext.Categories.Update(existingCategory);
        _appDbContext.SaveChanges();

        var updatedCategory = CategoryMapping.MapCategoryRes(existingCategory);
        return Task.FromResult(updatedCategory);
    }
}
