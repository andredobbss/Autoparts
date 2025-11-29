using AutoMapper;
using Autoparts.Api.Features.Returns.Infraestructure;
using MediatR;

namespace Autoparts.Api.Features.Returns.GetByIdQuery;

public sealed record GetReturnByIdQueryHandler(IReturnRepository returnRepository, IMapper mapper) : IRequestHandler<GetReturnByIdQuery, GetReturnByIdQueryResponse>
{
    private readonly IReturnRepository _returnRepository = returnRepository;
    private readonly IMapper _mapper = mapper;
    public async Task<GetReturnByIdQueryResponse> Handle(GetReturnByIdQuery request, CancellationToken cancellationToken)
    {
        var returnEntity = await _returnRepository.GetByIdAsync(request.ReturnId, cancellationToken);

        return _mapper.Map<GetReturnByIdQueryResponse>(returnEntity);
    }
}