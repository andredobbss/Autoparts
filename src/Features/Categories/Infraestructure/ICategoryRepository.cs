using Autoparts.Api.Features.Categories.Domain;
using FluentValidation.Results;
using Z.PagedList;

namespace Autoparts.Api.Features.Categories.Infraestructure;

public interface ICategoryRepository
{
    Task<IPagedList<Category>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
    Task<Category?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<ValidationResult> AddAsync(Category category, CancellationToken cancellationToken);
    Task<ValidationResult> UpdateAsync(Category category, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Category category, CancellationToken cancellationToken);
}
