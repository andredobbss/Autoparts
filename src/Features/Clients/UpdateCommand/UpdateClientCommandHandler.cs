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
            return new ValidationResult { Errors = { new ValidationFailure(Resource.CLIENT, string.Format(Resource.CLIENT_NOT_FOUND, request.ClientId)) } };

        client.Update(request.ClientName, request.Address);

        var result = await _clientRepository.UpdateAsync(client, cancellationToken);
        if (!result.IsValid)
            return result;

        var commitResult = await _clientRepository.CommitAsync(cancellationToken);
        if (!commitResult)
        {
            var failures = result.Errors.ToList();
            failures.Add(new ValidationFailure(Resource.COMMIT, Resource.COMMIT_FAILED_MESSAGE));
            return new ValidationResult(failures);
        }

        return result;
    }
}