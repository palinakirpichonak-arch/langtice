using System.Net;

namespace MainService.AL.Exceptions;

public class BadRequestException(string? message, IDictionary<string, string[]> errors) : DomainException(message)
{
    public IDictionary<string, string[]> Errors { get; } = errors;
    public override int StatusCode => (int)HttpStatusCode.BadRequest;
}