using Autoparts.Api.Features.Purchases.Dto;
using MediatR;

namespace Autoparts.Api.Features.Purchases.GetAllQuery;

public class GetPurchaseProductCategoryManufaturerQuery : IRequest<IEnumerable<PurchaseProductCategoryManufaturerDto>>
{
}
