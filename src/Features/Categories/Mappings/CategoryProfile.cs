using AutoMapper;
using Autoparts.Api.Features.Categories.Domain;
using Autoparts.Api.Features.Categories.GetAllQuery;
using Autoparts.Api.Features.Categories.GetByIdQuery;

namespace Autoparts.Api.Features.Categories.Mappings;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, GetAllCategoriesQueryResponse>().ReverseMap();
        CreateMap<Category, GetCategoryByIdQueryResponse>().ReverseMap();
    }
}
