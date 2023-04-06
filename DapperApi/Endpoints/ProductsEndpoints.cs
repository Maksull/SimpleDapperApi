using AutoMapper;
using DapperApi.Models;
using DapperApi.Models.Dto.Product;
using DapperApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DapperApi.Endpoints
{
    public static class ProductsEndpoints
    {
        public static IEndpointRouteBuilder MapProductsEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("api/mini/products", GetProducts);
            app.MapGet("api/mini/products/{id:long}", GetProduct);
            app.MapGet("api/mini/products/{id:long}/category", GetProductCategory);
            app.MapPost("api/mini/products", CreateProduct);
            app.MapPut("api/mini/products", UpdateProduct);
            app.MapDelete("api/mini/products", DeleteProduct);

            return app;
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Product>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async static Task<IResult> GetProducts(IProductService productService)
        {
            try
            {
                var products = await productService.GetProducts();

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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Product))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async static Task<IResult> GetProduct(long id, IProductService productService)
        {
            try
            {
                var product = await productService.GetProduct(id);

                if (product != null)
                {
                    return Results.Ok(product);
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
        public async static Task<IResult> GetProductCategory(long id, IProductService productService)
        {
            try
            {
                var category = await productService.GetProductCategory(id);

                if (category != null)
                {
                    return Results.Ok(category);
                }

                return Results.NotFound();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message); ;
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OkResult))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BadRequestResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async static Task<IResult> CreateProduct(ProductInsertDto request, IProductService productService, IMapper mapper)
        {
            try
            {
                Product product = mapper.Map<Product>(request);

                var changes = await productService.CreateProduct(product);

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
        public static async Task<IResult> UpdateProduct(ProductUpdateDto request, IProductService productService, IMapper mapper)
        {
            try
            {
                Product product = mapper.Map<Product>(request);

                var changes = await productService.UpdateProduct(product);

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
        public static async Task<IResult> DeleteProduct(long id, IProductService productService)
        {
            try
            {
                var changes = await productService.DeleteProduct(id);

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
