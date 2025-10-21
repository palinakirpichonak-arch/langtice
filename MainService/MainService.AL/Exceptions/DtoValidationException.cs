namespace MainService.AL.Exceptions;

public class DtoValidationException(Dictionary<string, string[]> error) : Exception
{
    public Dictionary<string, string[]> Errors { get; } = error;
}