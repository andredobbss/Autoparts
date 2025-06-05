using FluentValidation.Results;

namespace Autoparts.Api.Shared.Exceptions;

public sealed class DomainValidationException : Exception
{
    public List<ValidationFailure> Errors { get; }

    public DomainValidationException(string message, List<ValidationFailure> errors): base(message)
    {
        Errors = errors;
    }
}
