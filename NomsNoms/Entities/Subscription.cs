namespace NomsNoms.Entities
{
    public class Subscription
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public int Duration { get; set; }
        public ICollection<UserSubscription> UserSubscriptions { get; set; }
    }
}
