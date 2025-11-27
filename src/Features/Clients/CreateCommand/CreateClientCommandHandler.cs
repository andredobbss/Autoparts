using Autoparts.Api.Features.Clients.Domain;
using Autoparts.Api.Features.Clients.Infraestructure;
using Autoparts.Api.Shared.Resources;
using FluentValidation.Results;
using MediatR;

namespace Autoparts.Api.Features.Clients.CreateCommand;
public sealed class CreateClientCommandHandler(IClientRepository clientRepository) : IRequestHandler<CreateClientCommand, ValidationResult>
{
    private readonly IClientRepository _clientRepository = clientRepository;
    public async Task<ValidationResult> Handle(CreateClientCommand request, CancellationToken cancellationToken)
    {
        var client = new Client(request.ClientName, request.Address);

        var result = await _clientRepository.AddAsync(client, cancellationToken);
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