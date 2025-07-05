using Autoparts.Api.Features.Purchases.Dto;
using Autoparts.Api.Features.Purchases.Infraestructure;
using MediatR;

namespace Autoparts.Api.Features.Purchases.GetAllQuery
{
    public class GetPurchaseProductCategoryManufaturerQueryHandler(IPurchaseRepository purchaseRepository) : IRequestHandler<GetPurchaseProductCategoryManufaturerQuery, IEnumerable<PurchaseProductCategoryManufaturerDto>>
    {
        private readonly IPurchaseRepository _purchaseRepository = purchaseRepository;
        public async Task<IEnumerable<PurchaseProductCategoryManufaturerDto>> Handle(GetPurchaseProductCategoryManufaturerQuery request, CancellationToken cancellationToken)
        {
            return await _purchaseRepository.GetPurchaseProductCategoryManufaturerAsync(cancellationToken);
        }
    }
}
