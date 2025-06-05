using MediatR;
namespace Autoparts.Api.Features.Sales.CreateCommand;
public sealed class CreateSaleCommandHandler():IRequestHandler<CreateSaleCommand>
{
    public async Task Handle(CreateSaleCommand request, CancellationToken cancellationToken)
    {
         throw new NotImplementedException();
    }
}