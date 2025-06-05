using MediatR;

namespace Autoparts.Api.Features.Categories.GetAllQuery;

public sealed record GetAllCategoriesQueryHandler():IRequestHandler<GetAllCategoriesQuery,GetAllCategoriesQueryResponse>
{
    public async Task<GetAllCategoriesQueryResponse> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
         throw new NotImplementedException();
    }
}