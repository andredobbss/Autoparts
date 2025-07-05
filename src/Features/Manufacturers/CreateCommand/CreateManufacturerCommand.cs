using FluentValidation.Results;
using MediatR;
namespace Autoparts.Api.Features.Manufacturers.CreateCommand;

public sealed record CreateManufacturerCommand(string Description) : IRequest<ValidationResult>;