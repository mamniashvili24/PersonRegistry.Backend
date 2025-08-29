namespace PersonRegistry.Domain.Exceptions;

public class ApiException(string errorKey, object content = null) : Exception(errorKey)
{
    public string ErrorKey { get; set; } = errorKey;
    public object Content { get; set; } = content;
}