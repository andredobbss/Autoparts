using Autoparts.Api.Features.Returns.Infraestructure;
using Autoparts.Api.Shared.Enums;
using Autoparts.Api.Shared.Products.Stock;
using Autoparts.Api.Shared.Resources;
using FluentValidation.Results;
using MediatR;
namespace Autoparts.Api.Features.Returns.UpdateCommand;
public sealed class UpdateReturnCommandHandler(IReturnRepository returnRepository, IStockCalculator stockCalculator) : IRequestHandler<UpdateReturnCommand, ValidationResult>
{
    private readonly IReturnRepository _returnRepository = returnRepository;
    private readonly IStockCalculator _stockCalculator = stockCalculator;

    public async Task<ValidationResult> Handle(UpdateReturnCommand request, CancellationToken cancellationToken)
    {
        //if (request.Loss is true)
        //{
        //    var resultCalculationSubtraction = await _stockCalculator.CalculateStockAsync(request.ProductId, request.Quantity, ECalculationType.Subtraction, cancellationToken);
        //    if (resultCalculationSubtraction.IsValid is false)
        //        return resultCalculationSubtraction;

        //    var resultCalculationAddition = await _stockCalculator.CalculateStockAsync(request.ProductId, request.Quantity, ECalculationType.Addition, cancellationToken);
        //    if (resultCalculationAddition.IsValid is false)
        //        return resultCalculationAddition;
        //}

        var returnEntity = await _returnRepository.GetByIdAsync(request.ReturnId, cancellationToken);

        if (returnEntity is null)
            return new ValidationResult { Errors = { new ValidationFailure("Return", $"{Resource.ID_NOT_FOUND} : {request.ReturnId}") } };


        //returnEntity.Update(request.Justification,
        //                    request.InvoiceNumber,
        //                    request.Quantity,
        //                    request.Loss,
        //                    request.UserId,
        //                    request.ClientId);

        var result = await _returnRepository.UpdateAsync(returnEntity, cancellationToken);
        
        return result;

    }
}