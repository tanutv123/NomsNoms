namespace NomsNoms.Interfaces
{
    public interface ISubscriptionRepository
    {
        Task AddPaymentIntent(long orderCode, int userId, int subscriptionId);
    }
}
