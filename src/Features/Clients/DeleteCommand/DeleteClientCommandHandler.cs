using Autoparts.Api.Features.Clients.Infraestructure;
using Autoparts.Api.Shared.Resources;
using FluentValidation.Results;
using MediatR;

namespace Autoparts.Api.Features.Clients.DeleteCommand;
public sealed class DeleteClientCommandHandler(IClientRepository clientRepository) : IRequestHandler<DeleteClientCommand, ValidationResult>
{
    private readonly IClientRepository _clientRepository = clientRepository;

    public async Task<ValidationResult> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
    {
        var client = await _clientRepository.GetByIdAsync(request.ClientId, cancellationToken);
        if (client is null)
            return new ValidationResult([new ValidationFailure(Resource.CLIENT, string.Format(Resource.CLIENT_NOT_FOUND, request.ClientId))]);

        client.Delete();

        var deleted = await _clientRepository.DeleteAsync(client, cancellationToken);
        if (!deleted)
            return new ValidationResult([new ValidationFailure(Resource.CLIENT, Resource.FAILED_TO_DELETE_CLIENT)]);

        var committed = await _clientRepository.CommitAsync(cancellationToken);
        if (!committed)
            return new ValidationResult([new ValidationFailure(Resource.COMMIT, Resource.COMMIT_FAILED_MESSAGE)]);

        return new ValidationResult();
    }
}