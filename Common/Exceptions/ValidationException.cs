namespace YatraBackend.Common.Exceptions;

public class ValidationException(string message) : Exception(message:message)
{
}