using NomsNoms.Entities;

namespace NomsNoms.DTOs
{
    public class SubscriptionRecordDTO
    {
        public int SourceUserId { get; set; }
        public int TargetUserId { get; set; }
        public DateTime SubscriptionDuration { get; set; }
        public int SubscriptionId { get; set; }
    }
}
