using MediatR;
namespace Autoparts.Api.Features.Sales.UpdateCommand;
public sealed class UpdateSaleCommandHandler():IRequestHandler<UpdateSaleCommand>
{
    public async Task Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
    {
         throw new NotImplementedException();
    }
}