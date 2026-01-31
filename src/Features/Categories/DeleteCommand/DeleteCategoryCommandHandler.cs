using Autoparts.Api.Infraestructure.Persistence;
using Autoparts.Api.Shared.Resources;
using FluentValidation.Results;
using NetDevPack.SimpleMediator;

namespace Autoparts.Api.Features.Categories.DeleteCommand;

public sealed class DeleteCategoryCommandHandler(AutopartsDbContext _context) : IRequestHandler<DeleteCategoryCommand, ValidationResult>
{
    public async Task<ValidationResult> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _context.Categories!.FindAsync(request.CategoryId, cancellationToken);
        if (category is null)
            return new ValidationResult([new ValidationFailure(Resource.CATEGORY, string.Format(Resource.CATEGORY_NOT_FOUND, request.CategoryId))]);

        category.Delete();

        _context.Categories!.Update(category);

        var commitResult = await _context.SaveChangesAsync(cancellationToken);
        if (commitResult <= 0)
        {
            return new ValidationResult(
            [
                new ValidationFailure(Resource.COMMIT, Resource.COMMIT_FAILED_MESSAGE)
            ]);
        }

        return new ValidationResult();
    }
}