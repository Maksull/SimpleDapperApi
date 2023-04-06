using AutoMapper;
using DapperApi.Models;
using DapperApi.Models.Dto.Product;

namespace DapperApi.AutoMapper
{
    public sealed class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductInsertDto>();
            CreateMap<Product, ProductUpdateDto>();

            CreateMap<ProductInsertDto, Product>();
            CreateMap<ProductUpdateDto, Product>();

        }
    }
}
