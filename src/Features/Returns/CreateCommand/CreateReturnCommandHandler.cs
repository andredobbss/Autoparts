using MediatR;
namespace Autoparts.Api.Features.Returns.CreateCommand;
public sealed class CreateReturnCommandHandler():IRequestHandler<CreateReturnCommand>
{
    public async Task Handle(CreateReturnCommand request, CancellationToken cancellationToken)
    {
         throw new NotImplementedException();
    }
}