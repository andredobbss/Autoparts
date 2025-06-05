using MediatR;
namespace Autoparts.Api.Features.Returns.UpdateCommand;
public sealed class UpdateReturnCommandHandler():IRequestHandler<UpdateReturnCommand>
{
    public async Task Handle(UpdateReturnCommand request, CancellationToken cancellationToken)
    {
         throw new NotImplementedException();
    }
}