using MediatR;
namespace Autoparts.Api.Features.Suppliers.UpdateCommand;

public sealed record UpdateSupplierCommand(string Name) :IRequest;