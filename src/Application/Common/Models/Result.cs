namespace EventSystem.Application.Common.Models;

public class Result
{
    internal Result(bool succeeded, IEnumerable<string> messages, string? userId = null)
    {
        Succeeded = succeeded;
        Errors = messages.ToArray();
        UserId = userId;
    }

    public bool Succeeded { get; init; }

    public string[] Errors { get; init; }
    public string? UserId { get; init; }

    public static Result Success(string? userId = null, string? returnUrl = null)
    {
        return new Result(true, string.IsNullOrEmpty(returnUrl) ? Array.Empty<string>() : [returnUrl], userId);
    }

    public static Result Failure(IEnumerable<string> errors)
    {
        return new Result(false, errors);
    }
}
