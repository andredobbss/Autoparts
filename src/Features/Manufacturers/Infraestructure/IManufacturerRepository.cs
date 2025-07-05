using Autoparts.Api.Features.Manufacturers.Domain;
using FluentValidation.Results;
using Z.PagedList;

namespace Autoparts.Api.Features.Manufacturers.Infraestructure;

public interface IManufacturerRepository
{
    Task<IPagedList<Manufacturer>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
    Task<Manufacturer?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<ValidationResult> AddAsync(Manufacturer manufacturer, CancellationToken cancellationToken);
    Task<ValidationResult> UpdateAsync(Manufacturer manufacturer, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Manufacturer manufacturer, CancellationToken cancellationToken);  
}
