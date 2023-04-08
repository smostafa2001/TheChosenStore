namespace ShopManagement.Application.Contracts.Shared
{
    public class OperationResult
    {
        public bool IsSucceeded { get; set; }
        public string Message { get; set; }

        public OperationResult() => IsSucceeded = false;

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
}