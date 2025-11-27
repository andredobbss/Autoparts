using AutoMapper;
using Autoparts.Api.Features.Products.Domain;
using Autoparts.Api.Features.Products.GetAllQuery;
using Autoparts.Api.Features.Products.GetByIdQuery;

namespace Autoparts.Api.Features.Products.Mappings;

public class ProductsProfile : Profile
{
    public ProductsProfile()
    {
        CreateMap<Product, GetAllProductsQueryResponse>().ReverseMap();
        CreateMap<Product, GetProductByIdQueryResponse>().ReverseMap();
    }
}
