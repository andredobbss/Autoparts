using MediatR;
namespace Autoparts.Api.Features.Products.UpdateCommand;

public sealed record UpdateProductCommand(string Name) :IRequest;