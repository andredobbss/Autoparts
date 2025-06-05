using MediatR;
namespace Autoparts.Api.Features.Purchases.CreateCommand;
public sealed class CreatePurchaseCommandHandler():IRequestHandler<CreatePurchaseCommand>
{
    public async Task Handle(CreatePurchaseCommand request, CancellationToken cancellationToken)
    {
         throw new NotImplementedException();
    }
}