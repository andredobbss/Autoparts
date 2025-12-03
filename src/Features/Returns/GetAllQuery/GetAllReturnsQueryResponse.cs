using Autoparts.Api.Features.Returns.Domain;

namespace Autoparts.Api.Features.Returns.GetAllQuery;

public sealed record GetAllReturnsQueryResponse(Guid ReturnId,
                                                string Justification,
                                                string InvoiceNumber,
                                                DateTime CreatedAt,
                                                string UserName,
                                                string ClientName,
                                                IEnumerable<ReturnProduct> ReturnProducts);
