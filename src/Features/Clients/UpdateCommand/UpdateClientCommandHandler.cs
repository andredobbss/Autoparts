using Autoparts.Api.Features.Clients.Infraestructure;
using Autoparts.Api.Shared.Resources;
using FluentValidation.Results;
using MediatR;
namespace Autoparts.Api.Features.Clients.UpdateCommand;
public sealed class UpdateClientCommandHandler(IClientRepository clientRepository) : IRequestHandler<UpdateClientCommand, ValidationResult>
{
    private readonly IClientRepository _clientRepository = clientRepository;

    public async Task<ValidationResult> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
    {
        var client = await _clientRepository.GetByIdAsync(request.ClientId, cancellationToken);

        if (client is null)
            return new ValidationResult { Errors = { new ValidationFailure("Client", $"{Resource.ID_NOT_FOUND} : {request.ClientId}") } };

        client.Update(request.Name, request.Address);
        var result = await _clientRepository.UpdateAsync(client, cancellationToken);
        return result;
    }
}