using FluentValidation.Results;
using NetDevPack.SimpleMediator;

namespace Autoparts.Api.Features.Products.DeleteCommand;

public sealed record DeleteProductCommand(Guid ProductId) : IRequest<ValidationResult>;