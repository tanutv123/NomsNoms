namespace NomsNoms.Types
{
    public record CreateSubscriptionPaymentLinkRequest(
       int subscriptionId,
       string productName,
       string description,
       int price,
       string returnUrl,
       string cancelUrl
       );
}
