namespace Common.Application;

public class OperationResult
{
    public bool IsSucceeded { get; set; } = false;
    public string Message { get; set; } = string.Empty;

    public OperationResult Succeeded(string message = OperationMessages.IsSucceeded)
    {
        IsSucceeded = true;
        Message = message;
        return this;
    }

    public OperationResult Failed(string message)
    {
        IsSucceeded = false;
        Message = message;
        return this;
    }
}
