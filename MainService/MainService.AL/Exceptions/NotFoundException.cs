namespace MainService.AL.Exceptions;

public class NotFoundException(string name, string key) : Exception($"{name} ({key}) was not found");