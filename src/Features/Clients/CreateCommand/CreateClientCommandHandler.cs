using Autoparts.Api.Features.Clients.Domain;
using Autoparts.Api.Features.Clients.Infraestructure;
using FluentValidation.Results;
using MediatR;
namespace Autoparts.Api.Features.Clients.CreateCommand;
public sealed class CreateClientCommandHandler(IClientRepository clientRepository) : IRequestHandler<CreateClientCommand, ValidationResult>
{
    private readonly IClientRepository _clientRepository = clientRepository;
    public async Task<ValidationResult> Handle(CreateClientCommand request, CancellationToken cancellationToken)
    {
        var client = new Client(request.Name, request.Address);

        var result = await _clientRepository.AddAsync(client, cancellationToken);

        return result;
    }
}