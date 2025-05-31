using System;
using Carter;
using E_Commerce.Domain.DTOs.CategoryDTOs;
using E_Commerce.Handler.HandlerServices.InterfacesServices;
using E_Commerce.Handler.Wrapper.WorkWrapper;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Api.EndPoints.CategoryEndPoints;

public class GetCategoryByIdEndPoint : CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/category/get-by-id/{id:int}", async (int id , [FromServices] ICategoryService categoryService) =>
        {
            try
            {
                var categoryResult = await categoryService.GetCategoryByIdAsync(id  );
                if (categoryResult == null)
                {
                    return Results.NotFound("Category not found.");
                }
                return Results.Ok(Result<CategoryRes>.Success(categoryResult, "Category retrieved successfully."));
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message, statusCode: 500);
            }
        })
        .WithName("category-get-by-id")
        .WithTags("Category");
    }
}
