using Autoparts.Api.Features.Returns.Infraestructure;
using Autoparts.Api.Shared.Resources;
using FluentValidation.Results;
using MediatR;

namespace Autoparts.Api.Features.Returns.DeleteCommand;
public sealed class DeleteReturnCommandHandler(IReturnRepository returnRepository) : IRequestHandler<DeleteReturnCommand, ValidationResult>
{
    private readonly IReturnRepository _returnRepository = returnRepository;
    public async Task<ValidationResult> Handle(DeleteReturnCommand request, CancellationToken cancellationToken)
    {
        var returnItem = await _returnRepository.GetByIdAsync(request.ReturnId, cancellationToken);
        if (returnItem is null)
            return new ValidationResult([new ValidationFailure(Resource.RETURN, string.Format(Resource.RETURN_NOT_FOUND, request.ReturnId))]);

        returnItem.Delete();

        var deleted = await _returnRepository.DeleteAsync(returnItem, cancellationToken);
        if (!deleted)
            return new ValidationResult([new ValidationFailure(Resource.RETURN, Resource.FAILED_TO_DELETE_RETURN)]);

        var committed = await _returnRepository.CommitAsync(cancellationToken);
        if (!committed)
            return new ValidationResult([new ValidationFailure(Resource.COMMIT, Resource.COMMIT_FAILED_MESSAGE)]);

        return new ValidationResult();
    }
}