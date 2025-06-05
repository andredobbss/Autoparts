using MediatR;
namespace Autoparts.Api.Features.Purchases.UpdateCommand;
public sealed class UpdatePurchaseCommandHandler():IRequestHandler<UpdatePurchaseCommand>
{
    public async Task Handle(UpdatePurchaseCommand request, CancellationToken cancellationToken)
    {
         throw new NotImplementedException();
    }
}