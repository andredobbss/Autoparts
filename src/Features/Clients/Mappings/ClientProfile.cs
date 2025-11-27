using AutoMapper;
using Autoparts.Api.Features.Clients.Domain;
using Autoparts.Api.Features.Clients.GetAllQuery;
using Autoparts.Api.Features.Clients.GetByIdQuery;

namespace Autoparts.Api.Features.Clients.Mappings;

public class ClientProfile : Profile
{
    public ClientProfile()
    {
        CreateMap<Client, GetAllClientsQueryResponse>().ReverseMap();
        CreateMap<Client, GetClientByIdQueryResponse>().ReverseMap();
    }
}
