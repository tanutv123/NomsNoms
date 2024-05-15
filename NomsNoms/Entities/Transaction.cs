namespace NomsNoms.Entities
{
    public class Transaction
    {
        public int Id { get; set; }
        public string? ReportName { get; set; }
        public DateTime CreateDate { get; set; }
        public decimal TotalPrice { get; set; }
        public int SenderId { get; set; }
        public AppUser Sender { get; set; }
    }
}
