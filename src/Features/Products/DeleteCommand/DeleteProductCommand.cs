using MediatR;
namespace Autoparts.Api.Features.Products.DeleteCommand;

public sealed record DeleteProductCommand(Guid ProductId) : IRequest<bool>;