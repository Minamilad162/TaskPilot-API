namespace ProjectTaskManagement.Application.Common.Exceptions;

public sealed class ValidationException : Exception
{
    public ValidationException(string message) : base(message)
    {
        Errors = [message];
    }

    public ValidationException(IReadOnlyCollection<string> errors) : base("Validation failed.")
    {
        Errors = errors;
    }

    public IReadOnlyCollection<string> Errors { get; }
}
