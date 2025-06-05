using MediatR;
namespace Autoparts.Api.Features.Manufacturers.CreateCommand;
public sealed class CreateManufacturerCommandHandler():IRequestHandler<CreateManufacturerCommand>
{
    public async Task Handle(CreateManufacturerCommand request, CancellationToken cancellationToken)
    {
         throw new NotImplementedException();
    }
}