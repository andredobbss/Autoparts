using Autoparts.Api.Infraestructure.Persistence;
using Autoparts.Api.Shared.Resources;
using FluentValidation.Results;
using MediatR;

namespace Autoparts.Api.Features.Categories.DeleteCommand;

public sealed class DeleteCategoryCommandHandler(AutopartsDbContext context) : IRequestHandler<DeleteCategoryCommand, ValidationResult>
{
    private readonly AutopartsDbContext _context = context;
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