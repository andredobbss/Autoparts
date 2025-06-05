using MediatR;
namespace Autoparts.Api.Features.Purchases.DeleteCommand;
public sealed class DeletePurchaseCommandHandler():IRequestHandler<DeletePurchaseCommand>
{
    public async Task Handle(DeletePurchaseCommand request, CancellationToken cancellationToken)
    {
         throw new NotImplementedException();
    }
}