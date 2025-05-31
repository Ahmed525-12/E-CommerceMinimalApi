using System;
using Carter;
using E_Commerce.Domain.DTOs.CategoryDTOs;
using E_Commerce.Handler.HandlerServices.InterfacesServices;
using E_Commerce.Handler.Wrapper.WorkWrapper;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Api.EndPoints.CategoryEndPoints;

public class UpdateCategoryEndPoint : CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/category/update/{id:int}", async (int id, [FromBody] CategoryCreateReq categoryUpdateDTO, [FromServices] ICategoryService categoryService) =>
        {
            try
            {
                if (categoryUpdateDTO == null || id <= 0)
                {
                    return Results.BadRequest("Invalid category data.");
                }

                var updatedCategory = await categoryService.UpdateCategoryAsync(id, categoryUpdateDTO);
                if (updatedCategory == null)
                {
                    return Results.NotFound("Category not found.");
                }

                return Results.Ok(Result<CategoryRes>.Success(updatedCategory, "Category updated successfully."));
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message, statusCode: 500);
            }
        })
        .WithName("category-update")
        .WithTags("Category");
    }
}
