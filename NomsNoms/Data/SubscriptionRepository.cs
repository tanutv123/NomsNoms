using NomsNoms.Entities;
using NomsNoms.Interfaces;

namespace NomsNoms.Data
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly DataContext _context;

        public SubscriptionRepository(DataContext context)
        {
            _context = context;
        }
        public async Task AddPaymentIntent(long orderCode, int userId, int subscriptionId)
        {
            var payment = new SubscriptionPayment
            {
                OrderCode = orderCode,
                AppUserId = userId,
                SubscriptionId = subscriptionId,
                CreatedDate = DateTime.UtcNow,
            };
            await _context.SubscriptionPayments.AddAsync(payment);
            await _context.SaveChangesAsync();
        }
    }
}
