namespace NomsNoms.Types
{
    public record CreatePaymentLinkRequest(
        int mealPlanId,
        string productName,
        string description,
        int price,
        string returnUrl,
        string cancelUrl
        );
}
