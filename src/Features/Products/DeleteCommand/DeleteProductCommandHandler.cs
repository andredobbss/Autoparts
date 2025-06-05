using MediatR;
namespace Autoparts.Api.Features.Products.DeleteCommand;
public sealed class DeleteProductCommandHandler():IRequestHandler<DeleteProductCommand>
{
    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
         throw new NotImplementedException();
    }
}