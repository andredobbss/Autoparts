using MediatR;
namespace Autoparts.Api.Features.Returns.DeleteCommand;
public sealed class DeleteReturnCommandHandler():IRequestHandler<DeleteReturnCommand>
{
    public async Task Handle(DeleteReturnCommand request, CancellationToken cancellationToken)
    {
         throw new NotImplementedException();
    }
}