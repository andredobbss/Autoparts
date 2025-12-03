using FluentValidation.Results;
using MediatR;

namespace Autoparts.Api.Features.Products.CreateCommand;

public sealed record CreateProductCommand(string Name,
                                          string TechnicalDescription,
                                          string Compatibility,
                                          decimal AcquisitionCost,
                                          Guid CategoryId,
                                          Guid ManufacturerId) : IRequest<ValidationResult>;