using MediatR;
namespace Autoparts.Api.Features.Manufacturers.UpdateCommand;
public sealed class UpdateManufacturerCommandHandler():IRequestHandler<UpdateManufacturerCommand>
{
    public async Task Handle(UpdateManufacturerCommand request, CancellationToken cancellationToken)
    {
         throw new NotImplementedException();
    }
}