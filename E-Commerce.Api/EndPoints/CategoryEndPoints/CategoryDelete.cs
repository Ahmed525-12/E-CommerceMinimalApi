using System;
using Carter;
using E_Commerce.Handler.HandlerServices.InterfacesServices;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Api.EndPoints.CategoryEndPoints;

public class CategoryDelete : CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/category/delete/{id:int}", async ([FromServices] ICategoryService categoryService, int id) =>
        {
            try
            {
                var result = await categoryService.DeleteCategoryAsync(id);
                if (result)
                {
                    return Results.Ok("Category deleted successfully.");
                }
                else
                {
                    return Results.NotFound("Category not found.");
                }
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message, statusCode: 500);
            }
        })
        .WithName("category-delete")
        .WithTags("Category");
    }
}
