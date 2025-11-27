using FluentValidation.Results;
using MediatR;
namespace Autoparts.Api.Features.Products.UpdateCommand;

public sealed record UpdateProductCommand(Guid ProductId,
                                          string Name,
                                          string TechnicalDescription,
                                          string Compatibility,
                                          decimal AcquisitionCost,
                                          Guid CategoryId,
                                          Guid ManufacturerId) : IRequest<ValidationResult>;