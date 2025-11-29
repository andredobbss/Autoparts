using Autoparts.Api.Features.Purchases.Dto;
using MediatR;

namespace Autoparts.Api.Features.Purchases.GetAllQuery;

public sealed class GetPurchaseProductCategoryManufaturerQuery : IRequest<IEnumerable<PurchaseProductCategoryManufaturerDto>>
{
}
