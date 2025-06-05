using MediatR;
namespace Autoparts.Api.Features.Manufacturers.UpdateCommand;

public sealed record UpdateManufacturerCommand(string Name) :IRequest;