using MediatR;
namespace Autoparts.Api.Features.Sales.DeleteCommand;
public sealed class DeleteSaleCommandHandler():IRequestHandler<DeleteSaleCommand>
{
    public async Task Handle(DeleteSaleCommand request, CancellationToken cancellationToken)
    {
         throw new NotImplementedException();
    }
}