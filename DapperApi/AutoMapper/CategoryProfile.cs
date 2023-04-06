using AutoMapper;
using DapperApi.Models;
using DapperApi.Models.Dto.Category;

namespace DapperApi.AutoMapper
{
    public sealed class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryInsertDto>();
            CreateMap<Category, CategoryUpdateDto>();

            CreateMap<CategoryInsertDto, Category>();
            CreateMap<CategoryUpdateDto, Category>();

        }
    }
}
