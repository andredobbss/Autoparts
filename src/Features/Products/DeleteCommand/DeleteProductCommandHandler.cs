using Autoparts.Api.Features.Products.Infraestructure;
using MediatR;

namespace Autoparts.Api.Features.Products.DeleteCommand;

public sealed class DeleteProductCommandHandler(IProductRepository productRepository) : IRequestHandler<DeleteProductCommand, bool>
{
    private readonly IProductRepository _productRepository = productRepository;
    public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.ProductId, cancellationToken);
        if (product is null)
            return false;

        product.Delete();

        var deleted = await _productRepository.DeleteAsync(product, cancellationToken);
        if (!deleted)
            return false;

        var committed = await _productRepository.CommitAsync(cancellationToken);
        return committed;
    }
}