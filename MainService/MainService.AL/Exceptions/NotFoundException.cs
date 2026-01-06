using System.Net;

namespace MainService.AL.Exceptions;

public class NotFoundException(string message) : DomainException(message)
{
    public override int StatusCode => (int)HttpStatusCode.NotFound;
}