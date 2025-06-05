using Autoparts.Api.Features.Products.Domain;
using MediatR;
namespace Autoparts.Api.Features.Products.CreateCommand;

public sealed record CreateProductCommand(string TechnicalDescription,
                                          string Compatibility,
                                          decimal AcquisitionCost,
                                          Guid CategoryId,
                                          Guid ManufacturerId) : IRequest<Product>;