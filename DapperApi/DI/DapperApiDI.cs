using DapperApi.Data.Repository;
using DapperApi.Data.Repository.Interfaces;
using DapperApi.Data.UnitOfWork;
using DapperApi.Models.Dto.Category;
using DapperApi.Models.Dto.Product;
using DapperApi.Services;
using DapperApi.Services.Interfaces;
using DapperApi.Validators.Category;
using DapperApi.Validators.Product;
using FluentValidation;
using FluentValidation.AspNetCore;
using Serilog;

namespace DapperApi.DI
{
    public static class DapperApiDI
    {
        public static void ConfigureDI(this WebApplicationBuilder builder)
        {
            builder.Services.ConfigureServices();
            builder.Services.ConfigureFluentValidation();
            builder.Services.ConfigureUnitOfWork();

            builder.ConfigureLogging();
        }

        private static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();

        }

        private static void ConfigureFluentValidation(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();
            
            services.AddScoped<IValidator<ProductInsertDto>, ProductInsertDtoValidator>();
            services.AddScoped<IValidator<ProductUpdateDto>, ProductUpdateDtoValidator>();
            services.AddScoped<IValidator<CategoryInsertDto>, CategoryInsertDtoValidator>();
            services.AddScoped<IValidator<CategoryUpdateDto>, CategoryUpdateDtoValidator>();

        }

        private static void ConfigureUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IProductRepository, ProductRepository>()
                .AddScoped(provider => new Lazy<IProductRepository>(() => provider.GetRequiredService<IProductRepository>()));
            services.AddScoped<ICategoryRepository, CategoryRepository>()
                .AddScoped(provider => new Lazy<ICategoryRepository>(() => provider.GetRequiredService<ICategoryRepository>()));

        }

        private static void ConfigureLogging(this WebApplicationBuilder builder)
        {
            var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .Enrich.FromLogContext()
                .CreateLogger();

            builder.Logging.ClearProviders();
            builder.Logging.AddSerilog(logger);
        }
    }
}
