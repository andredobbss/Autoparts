using AutoMapper;
using Autoparts.Api.Features.Sales.GetAllQuery;
using Autoparts.Api.Features.Sales.GetByIdQuery;

namespace Autoparts.Api.Features.Sales.Mappings;

public class SaleProfile : Profile
{
    public SaleProfile()
    {
        CreateMap<SaleProfile, GetAllSalesQueryResponse>().ReverseMap();
        CreateMap<SaleProfile, GetSaleByIdQueryResponse>().ReverseMap();
    }
}
