using Autoparts.Api.Features.Purchases.Domain;
using Autoparts.Api.Features.Purchases.Dto;
using FluentValidation.Results;
using Z.PagedList;

namespace Autoparts.Api.Features.Purchases.Infraestructure;

public interface IPurchaseRepository
{
    Task<IPagedList<Purchase>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
    Task<IEnumerable<PurchaseProductCategoryManufaturerDto>> GetPurchaseProductCategoryManufaturerAsync(CancellationToken cancellationToken);
    Task<Purchase?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<ValidationResult> AddAsync(Purchase purchase, CancellationToken cancellationToken);
    Task<ValidationResult> UpdateAsync(Purchase purchase, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Purchase purchase, CancellationToken cancellationToken);
    Task<bool> CommitAsync(CancellationToken cancellationToken);

}
