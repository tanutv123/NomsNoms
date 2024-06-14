namespace NomsNoms.DTOs
{
    public class TransactionAdminDTO
    {
        public int Id { get; set; }
        public string? ReportName { get; set; }
        public DateTime CreateDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string Sender { get; set; }
    }
}
