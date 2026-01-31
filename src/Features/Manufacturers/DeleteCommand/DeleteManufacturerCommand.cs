using FluentValidation.Results;
using NetDevPack.SimpleMediator;

namespace Autoparts.Api.Features.Manufacturers.DeleteCommand;

public sealed record DeleteManufacturerCommand(Guid ManufacturerId) : IRequest<ValidationResult>;