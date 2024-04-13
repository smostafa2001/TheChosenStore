namespace Common.Application.ZarinPal;

public class PaymentRequest
{
    public string Mobile { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string CallbackURL { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Amount { get; set; }
    public string MerchantID { get; set; } = string.Empty;
}
