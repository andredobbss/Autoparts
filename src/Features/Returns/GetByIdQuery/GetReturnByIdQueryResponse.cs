using Autoparts.Api.Features.Returns.Domain;

namespace Autoparts.Api.Features.Returns.GetByIdQuery;

public sealed record GetReturnByIdQueryResponse(Guid ReturnId,
                                                string Justification,
                                                string InvoiceNumber,
                                                DateTime CreatedAt,
                                                string UserName,
                                                string ClientName,
                                                IEnumerable<ReturnProduct> ReturnProducts);
