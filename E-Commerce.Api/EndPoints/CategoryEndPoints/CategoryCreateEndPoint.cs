using System;
using System.ComponentModel.DataAnnotations;
using Carter;
using E_Commerce.Domain.DTOs.CategoryDTOs;
using E_Commerce.Handler.HandlerServices.InterfacesServices;
using E_Commerce.Handler.Wrapper.WorkWrapper;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Api.EndPoints.CategoryEndPoints;

public class CategoryCreateEndPoint : CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/category/create", async ([FromServices] ILogger<CategoryCreateEndPoint> logger, [FromServices] ICategoryService categoryService, [FromBody] CategoryCreateReq category) =>
        {
            try
            {
                // Validate the request DTO
                var validationResults = new List<ValidationResult>();
                var validationContext = new ValidationContext(category);
                bool isValid = Validator.TryValidateObject(category, validationContext, validationResults, true);

                if (!isValid)
                {
                    var errors = validationResults.Select(vr => vr.ErrorMessage).ToList();
                    var errorMessage = string.Join("; ", errors);

                    logger.LogWarning("Register validation failed: {Errors}", errorMessage);
                    return Results.BadRequest(Result<CategoryRes>.Fail(errorMessage));
                }
                var createdCategory = await categoryService.CreateCategoryAsync(category);
                if (createdCategory == null)
                {
                    logger.LogWarning("Category creation failed.");
                    return Results.BadRequest(Result<CategoryRes>.Fail("Category creation failed."));
                }

                return Results.Ok(Result<CategoryRes>.Success(createdCategory, "Category created successfully."));
            }
            catch (System.Exception ex)
            {

                logger.LogError(ex, "An error occurred while creating the category.");
                return Results.Ok(Result<CategoryRes>.Fail(ex.Message));
            }
        })
 .WithName("category-create")
        .WithTags("Category")
        ;
    }
}
