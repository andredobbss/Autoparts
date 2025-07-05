using MediatR;
namespace Autoparts.Api.Features.Suppliers.DeleteCommand;

public sealed record DeleteSupplierCommand(Guid SupplierId) : IRequest<bool>;