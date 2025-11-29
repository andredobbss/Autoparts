using Autoparts.Api.Features.Products.Domain;

namespace Autoparts.Api.Features.Returns.GetAllQuery;

public sealed record GetAllReturnsQueryResponse(Guid ReturnId,
                                                string Justification,
                                                string InvoiceNumber,
                                                DateTime CreatedAt,
                                                DateTime? UpdatedAt,
                                                Guid UserId,
                                                Guid ClientId,
                                                IEnumerable<Product> Products);
