namespace YatraBackend.Common.Exceptions;

public class NotFoundException(string message) : Exception(message: message)
{
}