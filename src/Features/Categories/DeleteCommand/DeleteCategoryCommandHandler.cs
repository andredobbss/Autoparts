using MediatR;
namespace Autoparts.Api.Features.Categories.DeleteCommand;
public sealed class DeleteCategoryCommandHandler():IRequestHandler<DeleteCategoryCommand>
{
    public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
         throw new NotImplementedException();
    }
}