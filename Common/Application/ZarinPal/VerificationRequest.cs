namespace Common.Application.ZarinPal;

public class VerificationRequest
{
    public int Amount { get; set; }
    public string MerchantID { get; set; } = string.Empty;
    public string Authority { get; set; } = string.Empty;
}
