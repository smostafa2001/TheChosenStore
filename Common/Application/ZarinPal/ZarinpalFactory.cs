using Microsoft.Extensions.Configuration;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;

namespace Common.Application.ZarinPal;

public class ZarinPalFactory(IConfiguration configuration) : IZarinPalFactory
{

    public string Prefix { get; set; } = configuration.GetSection("Payment")["Method"]!;
    private string MerchantId { get; } = configuration.GetSection("Payment")["Merchant"]!;

    public PaymentResponse CreatePaymentRequest(string amount, string mobile, string email, string description, long orderId)
    {
        amount = amount.Replace(",", "");
        var finalAmount = int.Parse(amount);
        var siteUrl = configuration.GetSection("payment")["siteUrl"];

        var client = new RestClient($"https://{Prefix}.zarinpal.com/pg/rest/WebGate/PaymentRequest.json");
        var request = new RestRequest { Method = Method.Post };
        request.AddHeader("Content-Type", "application/json");
        var body = new PaymentRequest
        {
            Mobile = mobile,
            CallbackURL = $"{siteUrl}/Checkout?handler=CallBack&oId={orderId}",
            Description = description,
            Email = email,
            Amount = finalAmount,
            MerchantID = MerchantId
        };

        request.AddJsonBody(body);
        var response = client.Execute(request);
        var jsonSerializer = new JsonNetSerializer();
        return jsonSerializer.Deserialize<PaymentResponse>(response)!;
    }

    public VerificationResponse CreateVerificationRequest(string authority, string amount)
    {
        var client = new RestClient($"https://{Prefix}.zarinpal.com/pg/rest/WebGate/PaymentVerification.json");
        var request = new RestRequest { Method = Method.Post };
        request.AddHeader("Content-Type", "application/json");

        amount = amount.Replace(",", "");
        var finalAmount = int.Parse(amount);

        request.AddJsonBody(new VerificationRequest
        {
            Amount = finalAmount,
            MerchantID = MerchantId,
            Authority = authority
        });
        var response = client.Execute(request);
        var jsonSerializer = new JsonNetSerializer();
        return jsonSerializer.Deserialize<VerificationResponse>(response)!;
    }
}