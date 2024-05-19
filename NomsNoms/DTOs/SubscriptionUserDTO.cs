namespace NomsNoms.DTOs
{
    public class SubscriptionUserDTO
    {
        public int SubscriptionId { get; set; }
        public string UserKnownAs { get; set; }
        public string UserEmail { get; set; }
        public decimal Price { get; set; }
        public int Duration { get; set; }
    }
}
