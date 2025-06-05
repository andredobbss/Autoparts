using MediatR;
namespace Autoparts.Api.Features.Suppliers.CreateCommand;

public sealed record CreateSupplierCommand(string Name) :IRequest;