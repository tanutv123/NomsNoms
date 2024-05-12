namespace NomsNoms.Entities
{
    public class AppUserSubscriptionRecord
    {
        public int SourceUserId { get; set; }
        public AppUser SourceUser { get; set; }
        public int TargetUserId { get; set; }
        public AppUser TargetUser { get; set; }
        public DateTime SubscriptionDuration {  get; set; } 
        public int SubscriptionId { get; set; }
        public Subscription Subscription { get; set; }
    }
}
