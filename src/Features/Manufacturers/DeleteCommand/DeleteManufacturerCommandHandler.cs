using Autoparts.Api.Features.Manufacturers.Infraestructure;
using MediatR;
namespace Autoparts.Api.Features.Manufacturers.DeleteCommand;
public sealed class DeleteManufacturerCommandHandler(IManufacturerRepository manufacturerRepository) : IRequestHandler<DeleteManufacturerCommand, bool>
{
    private readonly IManufacturerRepository _manufacturerRepository = manufacturerRepository;

    public async Task<bool> Handle(DeleteManufacturerCommand request, CancellationToken cancellationToken)
    {
        var manufacturer = await _manufacturerRepository.GetByIdAsync(request.ManufacturerId, cancellationToken);
        if (manufacturer is null)
            return false;

        manufacturer.Delete();

        var result = await _manufacturerRepository.DeleteAsync(manufacturer, cancellationToken);
        return result;
    }
}