using MediatR;
namespace Autoparts.Api.Features.Products.DeleteCommand;

public sealed record DeleteProductCommand(string Name) :IRequest;