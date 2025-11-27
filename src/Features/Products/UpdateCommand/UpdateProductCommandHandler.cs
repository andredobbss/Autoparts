using Autoparts.Api.Features.Products.Infraestructure;
using Autoparts.Api.Shared.Resources;
using FluentValidation.Results;
using MediatR;

namespace Autoparts.Api.Features.Products.UpdateCommand;
public sealed class UpdateProductCommandHandler(IProductRepository productRepository) : IRequestHandler<UpdateProductCommand, ValidationResult>
{
    private readonly IProductRepository _productRepository = productRepository;
    public async Task<ValidationResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.ProductId, cancellationToken);

        if (product is null)
            return new ValidationResult { Errors = { new ValidationFailure("Product", $"{Resource.ID_NOT_FOUND} : {request.ProductId}") } };

        product.Update(request.Name,
                        request.TechnicalDescription,
                        request.Compatibility,
                        request.AcquisitionCost,
                        request.CategoryId,
                        request.ManufacturerId);

        var result = await _productRepository.UpdateAsync(product, cancellationToken);

        if (!result.IsValid)
            return result;

        var commitResult = await _productRepository.CommitAsync(cancellationToken);

        if (!commitResult)
        {
            var failures = result.Errors.ToList();
            failures.Add(new ValidationFailure(Resource.COMMIT, Resource.COMMIT_FAILED_MESSAGE));
            return new ValidationResult(failures);
        }

        return result;

    }
}