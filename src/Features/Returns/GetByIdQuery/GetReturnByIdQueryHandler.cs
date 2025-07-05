using Autoparts.Api.Features.Returns.Domain;
using Autoparts.Api.Features.Returns.Infraestructure;
using MediatR;

namespace Autoparts.Api.Features.Returns.GetByIdQuery;

public sealed record GetReturnByIdQueryHandler(IReturnRepository returnRepository) :IRequestHandler<GetReturnByIdQuery,Return>
{
    private readonly IReturnRepository _returnRepository = returnRepository;
    public async Task<Return> Handle(GetReturnByIdQuery request, CancellationToken cancellationToken)
    {
        return await _returnRepository.GetByIdAsync(request.ReturnId, cancellationToken);
    }
}