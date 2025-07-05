using Autoparts.Api.Features.Users.Domain;
using Autoparts.Api.Infraestructure.Persistence;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Autoparts.Api.Features.Users.Infraestructure;

public sealed class UserRepository : IUserRepository
{
    private readonly AutopartsDbContext _context;
    private readonly UserManager<User> _userManager;
    private readonly IValidator<User> _validator;

    public UserRepository(AutopartsDbContext context, UserManager<User> userManager, IValidator<User> validator)
    {
        _context = context;
        _userManager = userManager;
        _validator = validator;
    }

    public async Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken)
    {
      
        return await _context.Users.ToListAsync(cancellationToken);
    }
}
