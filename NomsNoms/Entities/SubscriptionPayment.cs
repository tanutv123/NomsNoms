using System.ComponentModel.DataAnnotations;

namespace NomsNoms.Entities
{
    public class SubscriptionPayment
    {
        [Key]
        public long OrderCode { get; set; }
        public DateTime CreatedDate { get; set; }
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public int SubscriptionId { get; set; }
        public Subscription Subscription { get; set; }

    }
}
