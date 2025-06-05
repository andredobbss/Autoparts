using MediatR;
namespace Autoparts.Api.Features.Products.UpdateCommand;
public sealed class UpdateProductCommandHandler():IRequestHandler<UpdateProductCommand>
{
    public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
         throw new NotImplementedException();
    }
}