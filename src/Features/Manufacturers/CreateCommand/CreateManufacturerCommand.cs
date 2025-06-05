using MediatR;
namespace Autoparts.Api.Features.Manufacturers.CreateCommand;

public sealed record CreateManufacturerCommand(string Name) :IRequest;