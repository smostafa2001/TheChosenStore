namespace Common.Application.ZarinPal;

public class PaymentResult
{
    public bool IsSuccessful { get; set; }
    public string Message { get; set; } = string.Empty;
    public string IssueTrackingNo { get; set; } = string.Empty;

    public PaymentResult Succeeded(string message, string issueTrackingNo)
    {
        IsSuccessful = true;
        Message = message;
        IssueTrackingNo = issueTrackingNo;
        return this;
    }

    public PaymentResult Failed(string message)
    {
        Message = message;
        IsSuccessful = false;
        return this;
    }
}