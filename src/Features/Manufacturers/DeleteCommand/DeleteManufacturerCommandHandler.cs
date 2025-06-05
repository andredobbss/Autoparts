using MediatR;
namespace Autoparts.Api.Features.Manufacturers.DeleteCommand;
public sealed class DeleteManufacturerCommandHandler():IRequestHandler<DeleteManufacturerCommand>
{
    public async Task Handle(DeleteManufacturerCommand request, CancellationToken cancellationToken)
    {
         throw new NotImplementedException();
    }
}