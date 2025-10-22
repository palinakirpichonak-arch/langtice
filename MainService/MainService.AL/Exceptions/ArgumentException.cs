using System.Net;

namespace MainService.AL.Exceptions;

public class BadArgumentException(string message) : DomainException(message)
{
    public override int StatusCode => (int)HttpStatusCode.BadRequest;
}