using Autoparts.Api.Features.Users.Domain;
using Autoparts.Api.Infraestructure.Persistence;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Autoparts.Api.Features.Users.Infraestructure;

public sealed class UserRepository : IUserRepository, IDisposable
{
    private readonly AutopartsDbContext _context;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IValidator<User> _validator;

    public UserRepository(AutopartsDbContext context, UserManager<User> userManager, IValidator<User> validator, SignInManager<User> signInManager)
    {
        _context = context;
        _userManager = userManager;
        _validator = validator;
        _signInManager = signInManager;
    }

    public async Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Users.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Users.FindAsync(id, cancellationToken);
    }

    public async Task<IdentityResult> AddAsync(User user, CancellationToken cancellationToken)
    {
        var result = await _validator.ValidateAsync(user, cancellationToken);
        if (!result.IsValid)
        {
            var identityErrorList = new List<IdentityError>();
            identityErrorList.AddRange(result.Errors.Select(e => new IdentityError { Description = e.ErrorMessage }));
            return IdentityResult.Failed([.. identityErrorList]);
        }

        user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, user.PasswordHash!);

        return await _userManager.CreateAsync(user, user.PasswordHash!);
    }

    public async Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
    {
        var result = await _validator.ValidateAsync(user, cancellationToken);
        if (!result.IsValid)
        {
            var identityErrorList = new List<IdentityError>();
            identityErrorList.AddRange(result.Errors.Select(e => new IdentityError { Description = e.ErrorMessage }));
            return IdentityResult.Failed([.. identityErrorList]);
        }

        user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, user.PasswordHash!);

        return await _userManager.UpdateAsync(user);
    }

    public async Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
    {
        return await _userManager.DeleteAsync(user);
    }

    public async Task<SignInResult> LoginAsync(string userName, string password, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
            return SignInResult.Failed;

        return await _signInManager.PasswordSignInAsync(userName, password, true, false);
    }

    public async Task LogoutAsync()
    {
        await _signInManager.SignOutAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
        _userManager.Dispose();
    }


}

