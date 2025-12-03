using Autoparts.Api.Features.Returns.Domain;
using Autoparts.Api.Features.Returns.Infraestructure;
using Autoparts.Api.Shared.Resources;
using MediatR;

namespace Autoparts.Api.Features.Returns.GetByIdQuery;

public sealed record GetReturnByIdQueryHandler(IReturnRepository returnRepository) : IRequestHandler<GetReturnByIdQuery, GetReturnByIdQueryResponse>
{
    private readonly IReturnRepository _returnRepository = returnRepository;
    public async Task<GetReturnByIdQueryResponse> Handle(GetReturnByIdQuery request, CancellationToken cancellationToken)
    {
        var returnEntity = await _returnRepository.GetByIdAsync(request.ReturnId, cancellationToken);
        if (returnEntity is null)
            throw new KeyNotFoundException(string.Format(Resource.RETURN_NOT_FOUND, request.ReturnId));

        return new GetReturnByIdQueryResponse
        (
            returnEntity.ReturnId,
            returnEntity.Justification,
            returnEntity.InvoiceNumber,
            returnEntity.CreatedAt,
            returnEntity.User.UserName,
            returnEntity.Client.ClientName,
            returnEntity.ReturnProducts.Select(rp => new ReturnProduct
            (
                rp.Product.Name,
                rp.Product.SKU,
                rp.Quantity,
                rp.SellingPrice,
                rp.TotalItem,
                rp.Loss
            )).ToList()
        );
    }
}