using MediatR;
namespace Autoparts.Api.Features.Suppliers.CreateCommand;
public sealed class CreateSupplierCommandHandler():IRequestHandler<CreateSupplierCommand>
{
    public async Task Handle(CreateSupplierCommand request, CancellationToken cancellationToken)
    {
         throw new NotImplementedException();
    }
}