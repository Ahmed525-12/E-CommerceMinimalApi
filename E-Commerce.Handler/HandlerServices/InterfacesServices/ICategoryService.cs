using System;
using E_Commerce.Domain.DTOs.CategoryDTOs;

namespace E_Commerce.Handler.HandlerServices.InterfacesServices;

public interface ICategoryService
{
public Task<CategoryRes> CreateCategoryAsync(CategoryCreateReq category);
    public Task<CategoryRes> GetCategoryByIdAsync(int id);
    public Task<IEnumerable<CategoryRes>> GetAllCategoriesAsync();
    public Task<CategoryRes> UpdateCategoryAsync(int id, CategoryCreateReq category);
    public Task<bool> DeleteCategoryAsync(int id);
  
}
