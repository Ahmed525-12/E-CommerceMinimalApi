using System;
using Carter;
using E_Commerce.Domain.DTOs.CategoryDTOs;
using E_Commerce.Handler.HandlerServices.InterfacesServices;
using E_Commerce.Handler.Wrapper.WorkWrapper;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Api.EndPoints.CategoryEndPoints;

public class CategoryGetAll : CarterModule
{

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/categories", async ([FromServices] ICategoryService categoryService) =>
        {
            try
            {
                var categories = await categoryService.GetAllCategoriesAsync();
                if (categories == null || !categories.Any())
                {
                    return Results.NotFound("No categories found.");
                }

                return Results.Ok(Result<IEnumerable<CategoryRes>>.Success(categories, "Categories retrieved successfully."));
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message, statusCode: 500);
            }
        })
        .WithName("category-get-all")
        .WithTags("Category")
        ;
    }
}
