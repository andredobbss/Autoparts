using MediatR;
namespace Autoparts.Api.Features.Suppliers.UpdateCommand;
public sealed class UpdateSupplierCommandHandler():IRequestHandler<UpdateSupplierCommand>
{
    public async Task Handle(UpdateSupplierCommand request, CancellationToken cancellationToken)
    {
         throw new NotImplementedException();
    }
}