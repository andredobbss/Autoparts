using Autoparts.Api.Features.Returns.Domain;
using Autoparts.Api.Features.Returns.Infraestructure;
using Autoparts.Api.Shared.Enums;
using Autoparts.Api.Shared.Products.Stock;
using FluentValidation.Results;
using MediatR;
namespace Autoparts.Api.Features.Returns.CreateCommand;
public sealed class CreateReturnCommandHandler(IReturnRepository returnRepository, IStockCalculator stockCalculator) : IRequestHandler<CreateReturnCommand, ValidationResult>
{
    private readonly IReturnRepository _returnRepository = returnRepository;
    private readonly IStockCalculator _stockCalculator = stockCalculator;
    public async Task<ValidationResult> Handle(CreateReturnCommand request, CancellationToken cancellationToken)
    {
        //if (request.Loss is true)
        //{
        //    var resultCalculation = await _stockCalculator.CalculateStockAsync(request.ProductId, request.Quantity, ECalculationType.Addition, cancellationToken);
        //    if (resultCalculation.IsValid is false)
        //        return resultCalculation;
        //}

        //var returnDomain = new Return(request.Justification, request.InvoiceNumber, request.Quantity, request.Loss, request.UserId, request.ClientId);

        //var result = await _returnRepository.AddAsync(returnDomain, cancellationToken);
        //return result;

        throw new NotImplementedException();
    }
}