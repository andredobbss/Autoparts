using MediatR;
namespace Autoparts.Api.Features.Manufacturers.DeleteCommand;

public sealed record DeleteManufacturerCommand(string Name) :IRequest;