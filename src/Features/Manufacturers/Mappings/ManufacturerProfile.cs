using AutoMapper;
using Autoparts.Api.Features.Manufacturers.Domain;
using Autoparts.Api.Features.Manufacturers.GetAllQuery;
using Autoparts.Api.Features.Manufacturers.GetByIdQuery;

namespace Autoparts.Api.Features.Manufacturers.Mappings;

public class ManufacturerProfile : Profile
{
    public ManufacturerProfile()
    {
        CreateMap<Manufacturer, GetAllManufacturersQueryResponse>().ReverseMap();
        CreateMap<Manufacturer, GetManufacturerByIdQueryResponse>().ReverseMap();
    }
}
