using AutoMapper;
using Azure.Core;
using DapperApi.Data.UnitOfWork;
using DapperApi.Models;
using DapperApi.Models.Dto.Category;
using DapperApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DapperApi.Endpoints
{
    public static class CategoriesEndpoints
    {
        public static IEndpointRouteBuilder MapCategoriesEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("api/mini/categories", GetCategories);
            app.MapGet("api/mini/categories/all", GetAllCategories);
            app.MapGet("api/mini/categories/{id:long}", GetCategory);
            app.MapGet("api/mini/categories/{id:long}/products", GetCategoryProducts);
            app.MapPost("api/mini/categories", CreateCategory);
            app.MapPut("api/mini/categories", UpdateCategory);
            app.MapDelete("api/mini/categories", DeleteCategory);

            return app;
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Category>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public static async Task<IResult> GetCategories(ICategoryService categoryService)
        {
            try
            {
                var categories = await categoryService.GetCategories();

                if (categories.Any())
                {
                    return Results.Ok(categories);
                }

                return Results.NotFound();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Category>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public static async Task<IResult> GetAllCategories(ICategoryService categoryService)
        {
            try
            {
                var categories = await categoryService.GetAllCategories();

                if (categories.Any())
                {
                    return Results.Ok(categories);
                }

                return Results.NotFound();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Category))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public static async Task<IResult> GetCategory(long id, ICategoryService categoryService)
        {
            try
            {
                var category = await categoryService.GetCategory(id);

                if (category != null)
                {
                    return Results.Ok(category);
                }

                return Results.NotFound();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Product>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public static async Task<IResult> GetCategoryProducts(long id, ICategoryService categoryService)
        {
            try
            {
                var products = await categoryService.GetCategoryProducts(id);

                if (products.Any())
                {
                    return Results.Ok(products);
                }

                return Results.NotFound();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OkResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadRequestResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public static async Task<IResult> CreateCategory(CategoryInsertDto request, ICategoryService categoryService, IMapper mapper)
        {
            try
            {
                Category category = mapper.Map<Category>(request);

                var changes = await categoryService.CreateCategory(category);

                if (changes > 0)
                {
                    return Results.Ok();
                }

                return Results.BadRequest();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OkResult))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public static async Task<IResult> UpdateCategory(CategoryUpdateDto request, ICategoryService categoryService, IMapper mapper)
        {
            try
            {
                Category category = mapper.Map<Category>(request);

                var changes = await categoryService.UpdateCategory(category);

                if (changes > 0)
                {
                    return Results.Ok();
                }

                return Results.NotFound();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OkResult))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public static async Task<IResult> DeleteCategory(long id, ICategoryService categoryService)
        {
            try
            {
                var changes = await categoryService.DeleteCategory(id);

                if (changes > 0)
                {
                    return Results.Ok();
                }

                return Results.NotFound();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }
    }
}
