using Autoparts.Api.Features.Manufacturers.Domain;
using Autoparts.Api.Features.Manufacturers.Infraestructure;
using FluentValidation.Results;
using MediatR;
namespace Autoparts.Api.Features.Manufacturers.CreateCommand;
public sealed class CreateManufacturerCommandHandler(IManufacturerRepository manufacturerRepository) :IRequestHandler<CreateManufacturerCommand, ValidationResult>
{
    private readonly IManufacturerRepository _manufacturerRepository = manufacturerRepository;
    public async Task<ValidationResult> Handle(CreateManufacturerCommand request, CancellationToken cancellationToken)
    {
         var manufacturer = new Manufacturer(request.Description);

           var result = await _manufacturerRepository.AddAsync(manufacturer,cancellationToken);
           return result;
    }
}