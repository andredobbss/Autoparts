using Autoparts.Api.Features.Users.Domain;
using Microsoft.AspNetCore.Identity;

namespace Autoparts.Api.Features.Users.Infraestructure;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken);
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IdentityResult> AddAsync(User user, CancellationToken cancellationToken);
    Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken);
    Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken);
    Task<SignInResult> LoginAsync(string userName, string password, CancellationToken cancellationToken);
    Task LogoutAsync();

}
