using Autoparts.Api.Features.Users.Domain;
using Microsoft.AspNetCore.Identity;

namespace Autoparts.Api.Shared.Email;

public class NoOpEmailSender : IEmailSender<User>
{
    public Task SendConfirmationLinkAsync(User user, string email, string confirmationLink) =>
        Task.CompletedTask;

    public Task SendPasswordResetLinkAsync(User user, string email, string resetLink) =>
        Task.CompletedTask;

    public Task SendPasswordResetCodeAsync(User user, string email, string resetCode) =>
        Task.CompletedTask;
}
