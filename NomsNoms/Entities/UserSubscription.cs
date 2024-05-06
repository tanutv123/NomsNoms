namespace NomsNoms.Entities
{
    public class UserSubscription
    {
        public int SubscriptionId { get; set; }
        public int AppUserId { get; set; }
        public DateTime StartedDate { get; set; }
        public AppUser AppUser { get; set; }
        public Subscription Subscription { get; set; }
    }
}
