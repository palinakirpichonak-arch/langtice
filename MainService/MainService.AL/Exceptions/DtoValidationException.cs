using System.Net;

namespace MainService.AL.Exceptions;

public class DtoValidationException(Dictionary<string, string[]> error, string message) : DomainException(message)
{
    public Dictionary<string, string[]> Errors { get; } = error;
    public override int StatusCode => (int)HttpStatusCode.BadRequest;
}