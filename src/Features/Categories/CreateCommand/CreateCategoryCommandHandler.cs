using Autoparts.Api.Features.Categories.Domain;
using Autoparts.Api.Infraestructure.Persistence;
using Autoparts.Api.Shared.Resources;
using FluentValidation.Results;
using MediatR;

namespace Autoparts.Api.Features.Categories.CreateCommand;

public sealed class CreateCategoryCommandHandler(AutopartsDbContext context) : IRequestHandler<CreateCategoryCommand, ValidationResult>
{
    private readonly AutopartsDbContext _context = context;
    public async Task<ValidationResult> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new Category(request.Description);

        await _context.Categories!.AddAsync(category, cancellationToken);

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