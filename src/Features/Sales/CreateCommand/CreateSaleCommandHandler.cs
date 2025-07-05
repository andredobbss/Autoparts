using Autoparts.Api.Features.Sales.Infraestructure;
using FluentValidation.Results;
using MediatR;
namespace Autoparts.Api.Features.Sales.CreateCommand;
public sealed class CreateSaleCommandHandler(ISaleRepository saleRepository) :IRequestHandler<CreateSaleCommand, ValidationResult>
{
    private readonly ISaleRepository _saleRepository = saleRepository;
    public async Task<ValidationResult> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
    {
         throw new NotImplementedException();
    }
}