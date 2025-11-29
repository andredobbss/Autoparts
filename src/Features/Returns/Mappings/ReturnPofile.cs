using AutoMapper;
using Autoparts.Api.Features.Returns.Domain;
using Autoparts.Api.Features.Returns.GetAllQuery;
using Autoparts.Api.Features.Returns.GetByIdQuery;

namespace Autoparts.Api.Features.Returns.Mappings;

public class ReturnPofile : Profile
{
    public ReturnPofile()
    {
        CreateMap<Return, GetAllReturnsQueryResponse>().ReverseMap();
        CreateMap<Return, GetReturnByIdQueryResponse>().ReverseMap();
    }
}
