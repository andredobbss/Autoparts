using AutoMapper;
using Autoparts.Api.Features.Purchases.Domain;
using Autoparts.Api.Features.Purchases.GetAllQuery;

namespace Autoparts.Api.Features.Purchases.Msppings;

public class PurchaseProfile : Profile
{
    public PurchaseProfile()
    {
        CreateMap<Purchase, GetAllPurchasesQueryResponse>().ReverseMap();
    }
}
