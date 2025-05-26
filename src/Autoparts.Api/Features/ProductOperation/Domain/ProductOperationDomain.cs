using Autoparts.Api.Features.Product.Domain;
using Autoparts.Api.Features.ProductOperation.Domain.Enums;

namespace Autoparts.Api.Features.ProductOperation.Domain;

public class ProductOperationDomain
{
    protected ProductOperationDomain() { }

    public Guid ProductOperationId { get; private set; }
    public EOperationType OperationType { get; private set; }
    public Guid ProductId { get; private set; }
    public ProductDomain Products { get; private set; }

}
