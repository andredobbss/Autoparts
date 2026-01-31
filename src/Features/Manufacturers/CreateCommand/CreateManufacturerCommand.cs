using FluentValidation.Results;
using NetDevPack.SimpleMediator;

namespace Autoparts.Api.Features.Manufacturers.CreateCommand;

public sealed record CreateManufacturerCommand(string Description) : IRequest<ValidationResult>;