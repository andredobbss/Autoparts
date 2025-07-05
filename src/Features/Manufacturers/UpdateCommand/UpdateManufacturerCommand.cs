using FluentValidation.Results;
using MediatR;
namespace Autoparts.Api.Features.Manufacturers.UpdateCommand;

public sealed record UpdateManufacturerCommand(Guid ManufacturerId, string Description) : IRequest<ValidationResult>;