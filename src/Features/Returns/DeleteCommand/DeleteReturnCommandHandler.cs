using Autoparts.Api.Features.Returns.Infraestructure;
using MediatR;

namespace Autoparts.Api.Features.Returns.DeleteCommand;
public sealed class DeleteReturnCommandHandler(IReturnRepository returnRepository) : IRequestHandler<DeleteReturnCommand, bool>
{
    private readonly IReturnRepository _returnRepository = returnRepository;
    public async Task<bool> Handle(DeleteReturnCommand request, CancellationToken cancellationToken)
    {
        var returnItem = await _returnRepository.GetByIdAsync(request.ReturnId, cancellationToken);
        if (returnItem is null)
            return false;

        returnItem.Delete();

        var deleted = await _returnRepository.DeleteAsync(returnItem, cancellationToken);
        if (!deleted)
            return false;

        var committed = await _returnRepository.CommitAsync(cancellationToken);
        return committed;
    }
}