using Autoparts.Api.Features.Returns.Domain;
using Autoparts.Api.Features.Returns.Infraestructure;
using Autoparts.Api.Shared.Paginate;
using MediatR;

namespace Autoparts.Api.Features.Returns.GetAllQuery;

public sealed record GetAllReturnsQueryHandler(IReturnRepository returnRepository) :IRequestHandler<GetAllReturnsQuery, PagedResponse<Return>>
{
    private readonly IReturnRepository _returnRepository = returnRepository;
    public async Task<PagedResponse<Return>> Handle(GetAllReturnsQuery request, CancellationToken cancellationToken)
    {
        var returns = await _returnRepository.GetAllAsync(request.PageNumber, request.PageSize, cancellationToken);
        return new PagedResponse<Return>(returns);
    }
}